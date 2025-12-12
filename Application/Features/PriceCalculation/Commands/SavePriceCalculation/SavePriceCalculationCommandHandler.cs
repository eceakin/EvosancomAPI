using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Application.Repositories.PriceCalculation;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.PriceCalculation.Commands.SavePriceCalculation
{

	public class SavePriceCalculationCommandHandler : IRequestHandler<SavePriceCalculationCommandRequest, SavePriceCalculationCommandResponse>
	{
		private readonly IPriceCalculationHistoryWriteRepository _priceCalculationHistoryWriteRepository;
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IProductReadRepository _productReadRepository;
		private readonly ILogger<SavePriceCalculationCommandHandler> _logger;

		public SavePriceCalculationCommandHandler(
			IPriceCalculationHistoryWriteRepository priceCalculationHistoryWriteRepository,
			IDealerReadRepository dealerReadRepository,
			IProductReadRepository productReadRepository,
			ILogger<SavePriceCalculationCommandHandler> logger)
		{
			_priceCalculationHistoryWriteRepository = priceCalculationHistoryWriteRepository;
			_dealerReadRepository = dealerReadRepository;
			_productReadRepository = productReadRepository;
			_logger = logger;
		}

		public async Task<SavePriceCalculationCommandResponse> Handle(
			SavePriceCalculationCommandRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Saving price calculation for dealer {UserId}, product {ProductId}",
					request.UserId, request.ProductId);

				// 1. Dealer kontrolü
				var dealer = await _dealerReadRepository.GetAll(false)
					.FirstOrDefaultAsync(d => d.UserId == request.UserId && !d.IsDeleted, cancellationToken);

				if (dealer == null)
				{
					return new SavePriceCalculationCommandResponse
					{
						Success = false,
						Message = "Bayi bulunamadı."
					};
				}

				// 2. Ürün kontrolü
				var product = await _productReadRepository.GetByIdAsync(request.ProductId.ToString(), false);
				if (product == null)
				{
					return new SavePriceCalculationCommandResponse
					{
						Success = false,
						Message = "Ürün bulunamadı."
					};
				}

				// 3. Fiyat hesaplama kaydı oluştur
				var priceCalculation = new PriceCalculationHistory
				{
					DealerId = dealer.Id,
					ProductId = product.Id,
					Width = request.Width ?? 0,
					Height = request.Height ?? 0,
					Depth = request.Depth ?? 0,
					BasePrice = request.BasePrice,
					CustomDimensionCost = request.CustomDimensionCost,
					TotalPrice = request.TotalPrice,
					DiscountedPrice = request.DiscountedPrice,
					ConvertedToOrder = false,
					CreatedDate = DateTime.UtcNow
				};

				await _priceCalculationHistoryWriteRepository.AddAsync(priceCalculation);
				await _priceCalculationHistoryWriteRepository.SaveAsync();

				_logger.LogInformation("Price calculation saved successfully. Id: {Id}", priceCalculation.Id);

				return new SavePriceCalculationCommandResponse
				{
					Success = true,
					Message = "Fiyat hesaplaması kaydedildi.",
					CalculationId = priceCalculation.Id
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while saving price calculation");
				return new SavePriceCalculationCommandResponse
				{
					Success = false,
					Message = "Fiyat hesaplaması kaydedilirken bir hata oluştu: " + ex.Message
				};
			}
		}
	}
}
