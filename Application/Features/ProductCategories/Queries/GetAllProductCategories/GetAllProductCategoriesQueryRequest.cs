using MediatR;

namespace EvosancomAPI.Application.Features.ProductCategories.Queries.GetAllProductCategories
{
	public class GetAllProductCategoriesQueryRequest : IRequest<GetAllProductCategoriesQueryResponse>
	{
		public int PageNumber { get; set; } = 1;

		public int PageSize { get; set; } = 10;
	}
}
