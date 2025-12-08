using AutoMapper;
using AutoMapper.QueryableExtensions;
using EvosancomAPI.Application.Features.Products.DTOs;
using EvosancomAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvosancomAPI.Application.Features.Products.Queries.GetProducts
{
	public class GetProductsQueryHandler :
		IRequestHandler<GetProductsQueryRequest, GetProductsQueryResponse>
	{

		private readonly IProductReadRepository _productReadRepository;
		private readonly IMapper _mapper;
		readonly ILogger<GetProductsQueryHandler> _logger;
		public GetProductsQueryHandler(
			IProductReadRepository productReadRepository,
			IMapper mapper,
			ILogger<GetProductsQueryHandler> logger)
		{
			_productReadRepository = productReadRepository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<GetProductsQueryResponse> Handle(
			GetProductsQueryRequest request,
			CancellationToken cancellationToken)
		{
			_logger.LogInformation("Getting products. ");
			// Pagination için INDEX hesaplama
		//	throw new Exception("Bu bir deneme hatasıdır.");
			var skip = (request.PageNumber - 1) * request.PageSize;

			// Total count
			var totalCount = await _productReadRepository.GetAll(false).CountAsync();

			// Products
			var products = await _productReadRepository.GetAll(false)
				.Include(p => p.ProductCategory)
				.Skip(skip)
				.Take(request.PageSize)
				.ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

			return new GetProductsQueryResponse
			{
				Products = products,
				TotalProductCount = totalCount
			};
		}
	}
}
