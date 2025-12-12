using EvosancomAPI.Application.Features.DealerProducts.DTOs;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.DealerProducts.Queries.GetDealerProductById
{

	public class GetDealerProductByIdQueryHandler : IRequestHandler<GetDealerProductByIdQueryRequest, GetDealerProductByIdQueryResponse>
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly ILogger<GetDealerProductByIdQueryHandler> _logger;

		public GetDealerProductByIdQueryHandler(
			IProductReadRepository productReadRepository,
			IDealerReadRepository dealerReadRepository,
			ILogger<GetDealerProductByIdQueryHandler> logger)
		{
			_productReadRepository = productReadRepository;
			_dealerReadRepository = dealerReadRepository;
			_logger = logger;
		}

		public async Task<GetDealerProductByIdQueryResponse> Handle(
			GetDealerProductByIdQueryRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Fetching product {ProductId} for dealer {UserId}",
					request.ProductId, request.UserId);

				// 1. Dealer bilgisini getir
				var dealer = await _dealerReadRepository.GetAll(false)
					.FirstOrDefaultAsync(d => d.UserId == request.UserId && !d.IsDeleted, cancellationToken);

				if (dealer == null)
				{
					return new GetDealerProductByIdQueryResponse
					{
						Success = false,
						Message = "Bayi bulunamadı."
					};
				}

				// 2. Ürünü getir
				var product = await _productReadRepository.GetAll(false)
					.Include(p => p.ProductCategory)
					.FirstOrDefaultAsync(p => p.Id == request.ProductId && p.IsActive && !p.IsDeleted,
						cancellationToken);

				if (product == null)
				{
					_logger.LogWarning("Product not found: {ProductId}", request.ProductId);
					return new GetDealerProductByIdQueryResponse
					{
						Success = false,
						Message = "Ürün bulunamadı."
					};
				}

				// 3. İskontolu fiyatı hesapla
				var discountAmount = product.BasePrice * (dealer.DiscountRate / 100);
				var discountedPrice = product.BasePrice - discountAmount;

				var productDetail = new DealerProductDetailDto
				{
					ProductId = product.Id,
					Name = product.Name,
					Code = product.Code,
					Barcode = product.Barcode,
					CategoryId = product.ProductCategoryId,
					CategoryName = product.ProductCategory.Name,
					Description = product.Description,
					BasePrice = product.BasePrice,
					DiscountRate = dealer.DiscountRate,
					DiscountAmount = discountAmount,
					DiscountedPrice = discountedPrice,
					IsCustomizable = product.IsCustomizable,
					ImageUrl = product.ImageUrl,
					CreatedDate = product.CreatedDate
				};

				return new GetDealerProductByIdQueryResponse
				{
					Success = true,
					Product = productDetail
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching product by id");
				return new GetDealerProductByIdQueryResponse
				{
					Success = false,
					Message = "Ürün detayları getirilirken bir hata oluştu: " + ex.Message
				};
			}
		}
	}
}
