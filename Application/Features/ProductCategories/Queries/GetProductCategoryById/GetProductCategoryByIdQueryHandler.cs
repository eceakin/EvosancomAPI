using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using MediatR;

namespace EvosancomAPI.Application.Features.ProductCategories.Queries.GetProductCategoryById
{
	public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQueryRequest, GetProductCategoryByIdQueryResponse>
	{
		readonly IProductCategoryReadRepository _productCategoryReadRepository;

		public GetProductCategoryByIdQueryHandler(IProductCategoryReadRepository productCategoryReadRepository)
		{
			_productCategoryReadRepository = productCategoryReadRepository;
		}

		public async Task<GetProductCategoryByIdQueryResponse> Handle(GetProductCategoryByIdQueryRequest request, CancellationToken cancellationToken)
		{
			ProductCategory productCategory =await _productCategoryReadRepository.GetByIdAsync(request.Id, false);
			return new()
			{
				Name = productCategory.Name
			};
		}
	}
}
