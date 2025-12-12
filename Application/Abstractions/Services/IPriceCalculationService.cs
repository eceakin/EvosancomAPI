using EvosancomAPI.Application.Features.Orders.DTOs;
using EvosancomAPI.Application.Features.PriceCalculation.DTOs;

namespace EvosancomAPI.Application.Abstractions.Services
{

	public interface IPriceCalculationService
	{
		/// <summary>
		/// Ürün fiyatını hesaplar (özel ölçü ve bayi indirimi dahil)
		/// </summary>
		Task<PriceCalculationResultDto> CalculatePriceAsync(
			Guid productId,
			Guid dealerId,
			CustomDimensionDto customDimension = null);

		/// <summary>
		/// Özel ölçü maliyetini hesaplar
		/// </summary>
		Task<decimal> CalculateCustomDimensionCostAsync(
			Guid productId,
			CustomDimensionDto customDimension);

		/// <summary>
		/// Bayi iskontosunu uygular
		/// </summary>
		Task<decimal> ApplyDealerDiscountAsync(
			decimal basePrice,
			Guid dealerId);

		/// <summary>
		/// Detaylı fiyat dökümü oluşturur
		/// </summary>
		Task<PriceBreakdownDto> GetPriceBreakdownAsync(
			Guid productId,
			Guid dealerId,
			CustomDimensionDto customDimension = null);

		/// <summary>
		/// Sipariş için toplam tutarı hesaplar
		/// </summary>
		Task<decimal> CalculateOrderTotalAsync(
			List<CreateOrderItemDto> orderItems,
			Guid dealerId);

		/// <summary>
		/// Tahmini teslimat süresini hesaplar
		/// </summary>
		int CalculateEstimatedDeliveryDays(bool hasCustomDimension, int quantity = 1);

		/// <summary>
		/// Kübik hacim hesaplar (cm³)
		/// </summary>
		decimal CalculateVolume(decimal width, decimal height, decimal depth);
	}

}
