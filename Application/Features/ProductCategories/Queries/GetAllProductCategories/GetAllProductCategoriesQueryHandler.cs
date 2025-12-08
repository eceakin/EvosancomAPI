using AutoMapper;
using AutoMapper.QueryableExtensions;
using EvosancomAPI.Application.Features.ProductCategories.DTOs;
using EvosancomAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.ProductCategories.Queries.GetAllProductCategories
{
	public class GetAllProductCategoriesQueryHandler : IRequestHandler<GetAllProductCategoriesQueryRequest, GetAllProductCategoriesQueryResponse>
	{
		private readonly IProductCategoryReadRepository _productCategoryReadRepository;
		private readonly IMapper _mapper;

		public GetAllProductCategoriesQueryHandler(IProductCategoryReadRepository productCategoryReadRepository, IMapper mapper)
		{
			_productCategoryReadRepository = productCategoryReadRepository;
			_mapper = mapper;
		}
		public async Task<GetAllProductCategoriesQueryResponse> Handle(GetAllProductCategoriesQueryRequest request, CancellationToken cancellationToken)
		{
			var skip = (request.PageNumber - 1) * request.PageSize;
			var totalCount = await _productCategoryReadRepository.GetAll(false).CountAsync();
			var categories = await _productCategoryReadRepository.GetAll(false)
				.Skip(skip)
				.Take(request.PageSize)
				.ProjectTo<ProductCategoryDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
			return new GetAllProductCategoriesQueryResponse
			{
				ProductCategories = categories,
				TotalProductCategoryCount = totalCount
			};

		}
	}
}
