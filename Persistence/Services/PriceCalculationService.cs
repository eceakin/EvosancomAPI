using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.Features.Orders.DTOs;
using EvosancomAPI.Application.Features.PriceCalculation.DTOs;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Services
{
	public class PriceCalculationService : IPriceCalculationService
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IConfiguration _configuration;
		private readonly ILogger<PriceCalculationService> _logger;

		// Fiyat hesaplama sabitleri
		private const decimal UNIT_COST_PER_CUBIC_CM = 0.05m; // Her cm³ için maliyet (özelleştirilebilir)
		private const decimal COMPLEXITY_FACTOR = 1.2m; // Karmaşıklık faktörü
		private const decimal MIN_CUSTOM_COST = 100m; // Minimum özel ölçü maliyeti
		private const int BASE_DELIVERY_DAYS = 20; // Standart teslimat süresi
		private const int CUSTOM_DELIVERY_EXTRA_DAYS = 10; // Özel ölçü ekstra süresi

		public PriceCalculationService(
			IProductReadRepository productReadRepository,
			IDealerReadRepository dealerReadRepository,
			IConfiguration configuration,
			ILogger<PriceCalculationService> logger)
		{
			_productReadRepository = productReadRepository;
			_dealerReadRepository = dealerReadRepository;
			_configuration = configuration;
			_logger = logger;
		}

		public async Task<PriceCalculationResultDto> CalculatePriceAsync(
			Guid productId,
			Guid dealerId,
			CustomDimensionDto customDimension = null)
		{
			try
			{
				_logger.LogInformation("Calculating price for Product: {ProductId}, Dealer: {DealerId}",
					productId, dealerId);

				// 1. Ürünü getir
				var product = await _productReadRepository.GetByIdAsync(productId.ToString(), false);
				if (product == null)
					throw new Exception("Ürün bulunamadı.");

				// 2. Dealer'ı getir
				var dealer = await _dealerReadRepository.GetByIdAsync(dealerId.ToString(), false);
				if (dealer == null)
					throw new Exception("Bayi bulunamadı.");

				// 3. Base fiyat
				decimal basePrice = product.BasePrice;

				// 4. Özel ölçü maliyeti (varsa)
				decimal customDimensionCost = 0;
				if (customDimension != null && product.IsCustomizable)
				{
					customDimensionCost = await CalculateCustomDimensionCostAsync(productId, customDimension);
				}

				// 5. Toplam fiyat (base + custom)
				decimal totalPrice = basePrice + customDimensionCost;

				// 6. İskonto hesapla
				decimal discountRate = dealer.DiscountRate;
				decimal discountAmount = totalPrice * (discountRate / 100);
				decimal finalPrice = totalPrice - discountAmount;

				_logger.LogInformation("Price calculated - Base: {Base}, Custom: {Custom}, Discount: {Discount}, Final: {Final}",
					basePrice, customDimensionCost, discountAmount, finalPrice);

				return new PriceCalculationResultDto
				{
					ProductId = productId,
					ProductName = product.Name,
					BasePrice = basePrice,
					CustomDimensionCost = customDimensionCost,
					TotalPrice = totalPrice,
					DiscountRate = discountRate,
					DiscountAmount = discountAmount,
					FinalPrice = finalPrice,
					HasCustomDimensions = customDimension != null,
					CustomDimensions = customDimension
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in CalculatePriceAsync");
				throw;
			}
		}

		public async Task<decimal> CalculateCustomDimensionCostAsync(
			Guid productId,
			CustomDimensionDto customDimension)
		{
			try
			{
				if (customDimension == null)
					return 0;

				// 1. Ürünü kontrol et
				var product = await _productReadRepository.GetByIdAsync(productId.ToString(), false);
				if (product == null || !product.IsCustomizable)
					return 0;

				// 2. Hacim hesapla (cm³)
				decimal volume = CalculateVolume(
					customDimension.Width,
					customDimension.Height,
					customDimension.Depth);

				// 3. Maliyet formülü:
				// Cost = (Volume * UnitCostPerCubicCm * ComplexityFactor)
				decimal calculatedCost = volume * UNIT_COST_PER_CUBIC_CM * COMPLEXITY_FACTOR;

				// 4. Minimum maliyet kontrolü
				decimal finalCost = Math.Max(calculatedCost, MIN_CUSTOM_COST);

				_logger.LogInformation("Custom dimension cost calculated - Volume: {Volume} cm³, Cost: {Cost}",
					volume, finalCost);

				return Math.Round(finalCost, 2);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in CalculateCustomDimensionCostAsync");
				throw;
			}
		}

		public async Task<decimal> ApplyDealerDiscountAsync(decimal basePrice, Guid dealerId)
		{
			try
			{
				var dealer = await _dealerReadRepository.GetByIdAsync(dealerId.ToString(), false);
				if (dealer == null)
					return basePrice;

				decimal discountAmount = basePrice * (dealer.DiscountRate / 100);
				return basePrice - discountAmount;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in ApplyDealerDiscountAsync");
				throw;
			}
		}

		public async Task<PriceBreakdownDto> GetPriceBreakdownAsync(
			Guid productId,
			Guid dealerId,
			CustomDimensionDto customDimension = null)
		{
			try
			{
				var calculation = await CalculatePriceAsync(productId, dealerId, customDimension);

				var breakdown = new PriceBreakdownDto
				{
					BasePrice = calculation.BasePrice,
					CustomDimensionCost = calculation.CustomDimensionCost,
					Subtotal = calculation.TotalPrice,
					DiscountRate = calculation.DiscountRate,
					DiscountAmount = calculation.DiscountAmount,
					TotalPrice = calculation.FinalPrice,
					Details = new List<PriceDetailDto>()
				};

				// Detayları ekle
				breakdown.Details.Add(new PriceDetailDto
				{
					Label = "Ürün Baz Fiyatı",
					Amount = calculation.BasePrice,
					IsDiscount = false
				});

				if (calculation.CustomDimensionCost > 0)
				{
					breakdown.Details.Add(new PriceDetailDto
					{
						Label = "Özel Ölçü Maliyeti",
						Amount = calculation.CustomDimensionCost,
						IsDiscount = false,
						Description = $"{customDimension.Width}x{customDimension.Height}x{customDimension.Depth} cm"
					});
				}

				if (calculation.DiscountAmount > 0)
				{
					breakdown.Details.Add(new PriceDetailDto
					{
						Label = $"Bayi İndirimi (%{calculation.DiscountRate})",
						Amount = calculation.DiscountAmount,
						IsDiscount = true
					});
				}

				return breakdown;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in GetPriceBreakdownAsync");
				throw;
			}
		}

		public async Task<decimal> CalculateOrderTotalAsync(
			List<CreateOrderItemDto> orderItems,
			Guid dealerId)
		{
			try
			{
				decimal totalAmount = 0;

				foreach (var item in orderItems)
				{
					var calculation = await CalculatePriceAsync(
						item.ProductId,
						dealerId,
						item.HasCustomDimensions ? item.CustomDimension : null);

					totalAmount += calculation.FinalPrice * item.Quantity;
				}

				return totalAmount;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in CalculateOrderTotalAsync");
				throw;
			}
		}

		public int CalculateEstimatedDeliveryDays(bool hasCustomDimension, int quantity = 1)
		{
			int baseDays = BASE_DELIVERY_DAYS;

			// Özel ölçü varsa ekstra süre ekle
			if (hasCustomDimension)
			{
				baseDays += CUSTOM_DELIVERY_EXTRA_DAYS;
			}

			// Miktar fazlaysa ekstra süre (her 5 ürün için +2 gün)
			if (quantity > 5)
			{
				int extraDays = ((quantity - 5) / 5) * 2;
				baseDays += Math.Min(extraDays, 10); // Max 10 gün ekstra
			}

			return baseDays;
		}

		public decimal CalculateVolume(decimal width, decimal height, decimal depth)
		{
			return width * height * depth;
		}
	}
}
