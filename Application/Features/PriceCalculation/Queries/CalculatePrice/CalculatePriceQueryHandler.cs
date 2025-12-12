using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.PriceCalculation.Queries.CalculatePrice
{
	public class CalculatePriceQueryHandler : IRequestHandler<CalculatePriceQueryRequest, CalculatePriceQueryResponse>
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IPriceCalculationService _priceCalculationService;
		private readonly ILogger<CalculatePriceQueryHandler> _logger;

		public CalculatePriceQueryHandler(
			IProductReadRepository productReadRepository,
			IDealerReadRepository dealerReadRepository,
			IPriceCalculationService priceCalculationService,
			ILogger<CalculatePriceQueryHandler> logger)
		{
			_productReadRepository = productReadRepository;
			_dealerReadRepository = dealerReadRepository;
			_priceCalculationService = priceCalculationService;
			_logger = logger;
		}

		public async Task<CalculatePriceQueryResponse> Handle(
			CalculatePriceQueryRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Calculating price for product {ProductId}, dealer {UserId}",
					request.ProductId, request.UserId);

				// 1. Dealer kontrolü
				var dealer = await _dealerReadRepository.GetAll(false)
					.FirstOrDefaultAsync(d => d.UserId == request.UserId && !d.IsDeleted, cancellationToken);

				if (dealer == null)
				{
					return new CalculatePriceQueryResponse
					{
						Success = false,
						Message = "Bayi bulunamadı."
					};
				}

				// 2. Ürün kontrolü
				var product = await _productReadRepository.GetByIdAsync(request.ProductId.ToString(), false);
				if (product == null || !product.IsActive)
				{
					return new CalculatePriceQueryResponse
					{
						Success = false,
						Message = "Ürün bulunamadı veya aktif değil."
					};
				}

				// 3. Özel ölçü kontrolü
				if (request.CustomDimension != null && !product.IsCustomizable)
				{
					return new CalculatePriceQueryResponse
					{
						Success = false,
						Message = "Bu ürün özel ölçülerde üretilemiyor."
					};
				}

				// 4. Fiyat hesaplama servisi
				var calculation = await _priceCalculationService.CalculatePriceAsync(
					product.Id,
					dealer.Id,
					request.CustomDimension
				);

				_logger.LogInformation("Price calculated successfully. BasePrice: {BasePrice}, FinalPrice: {FinalPrice}",
					calculation.BasePrice, calculation.FinalPrice);

				return new CalculatePriceQueryResponse
				{
					Success = true,
					Calculation = calculation
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while calculating price");
				return new CalculatePriceQueryResponse
				{
					Success = false,
					Message = "Fiyat hesaplanırken bir hata oluştu: " + ex.Message
				};
			}
		}
	}
}
