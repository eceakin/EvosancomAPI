using AutoMapper;
using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Application.Repositories;
using MediatR;
using EvosancomAPI.Domain.Entities.Common;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using EvosancomAPI.Application.Features.DealerProducts.DTOs;

namespace EvosancomAPI.Application.Features.DealerProducts.Queries.GetDealerProducts
{
	public class GetDealerProductsQueryHandler : IRequestHandler<GetDealerProductsQueryRequest, GetDealerProductsQueryResponse>
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IPriceCalculationService _priceCalculationService;
		private readonly IMapper _mapper;
		private readonly ILogger<GetDealerProductsQueryHandler> _logger;

		public GetDealerProductsQueryHandler(
			IProductReadRepository productReadRepository,
			IDealerReadRepository dealerReadRepository,
			IPriceCalculationService priceCalculationService,
			IMapper mapper,
			ILogger<GetDealerProductsQueryHandler> logger)
		{
			_productReadRepository = productReadRepository;
			_dealerReadRepository = dealerReadRepository;
			_priceCalculationService = priceCalculationService;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<GetDealerProductsQueryResponse> Handle(
			GetDealerProductsQueryRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Fetching products for dealer: {UserId}", request.UserId);

				// 1. Dealer bilgisini getir
				var dealer = await _dealerReadRepository.GetAll(false)
					.FirstOrDefaultAsync(d => d.UserId == request.UserId && !d.IsDeleted, cancellationToken);

				if (dealer == null)
				{
					return new GetDealerProductsQueryResponse
					{
						Success = false,
						Message = "Bayi bulunamadı."
					};
				}

				// 2. Base query
				var query = _productReadRepository.GetAll(false)
					.Include(p => p.ProductCategory)
					.Where(p => p.IsActive && !p.IsDeleted);

				// 3. Kategori filtresi
				if (request.CategoryId.HasValue)
				{
					query = query.Where(p => p.ProductCategoryId == request.CategoryId.Value);
				}

				// 4. Arama
				if (!string.IsNullOrWhiteSpace(request.SearchTerm))
				{
					query = query.Where(p =>
						p.Name.Contains(request.SearchTerm) ||
						p.Code.Contains(request.SearchTerm) ||
						p.Description.Contains(request.SearchTerm));
				}

				// 5. Fiyat filtresi
				if (request.MinPrice.HasValue)
				{
					query = query.Where(p => p.BasePrice >= request.MinPrice.Value);
				}

				if (request.MaxPrice.HasValue)
				{
					query = query.Where(p => p.BasePrice <= request.MaxPrice.Value);
				}

				// 6. Özelleştirilebilir ürünler filtresi
				if (request.IsCustomizable.HasValue)
				{
					query = query.Where(p => p.IsCustomizable == request.IsCustomizable.Value);
				}

				// 7. Sıralama
				query = request.OrderBy?.ToLower() switch
				{
					"name_asc" => query.OrderBy(p => p.Name),
					"name_desc" => query.OrderByDescending(p => p.Name),
					"price_asc" => query.OrderBy(p => p.BasePrice),
					"price_desc" => query.OrderByDescending(p => p.BasePrice),
					"newest" => query.OrderByDescending(p => p.CreatedDate),
					_ => query.OrderBy(p => p.Name)
				};

				// 8. Toplam kayıt sayısı
				var totalCount = await query.CountAsync(cancellationToken);

				// 9. Pagination
				var products = await query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToListAsync(cancellationToken);

				// 10. DTO'ya çevir ve iskontolu fiyatı hesapla
				// DTO Mapping + Discount hesaplama
				var dealerProducts = products.Select(p =>
				{
					var discountRate = dealer.DiscountRate;
					var discountAmount = p.BasePrice * (discountRate / 100M);

					return new DealerProductDto
					{
						ProductId = p.Id,
						Name = p.Name,
						Code = p.Code,
						Barcode = p.Barcode,
						CategoryId = p.ProductCategoryId,
						CategoryName = p.ProductCategory.Name,
						Description = p.Description,
						BasePrice = p.BasePrice,
						DiscountRate = discountRate,
						DiscountAmount = discountAmount,
						DiscountedPrice = p.BasePrice - discountAmount,
						IsCustomizable = p.IsCustomizable,
						ImageUrl = p.ImageUrl
					};
				}).ToList();

				_logger.LogInformation("Found {Count} products for dealer", totalCount);

				return new GetDealerProductsQueryResponse
				{
					Success = true,
					DealerProducts = dealerProducts,
					DealerDiscountRate = dealer.DiscountRate,
					PaginationInfo = new PaginationInfo
					{
						CurrentPage = request.PageNumber,
						PageSize = request.PageSize,
						TotalCount = totalCount,
						TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
					}
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching dealer products");
				return new GetDealerProductsQueryResponse
				{
					Success = false,
					Message = "Ürünler getirilirken bir hata oluştu: " + ex.Message
				};
			}
		}
	}

}
