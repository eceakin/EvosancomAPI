using MediatR;

namespace EvosancomAPI.Application.Features.Products.Queries.GetProducts
{
	public class GetProductsQueryRequest : IRequest<GetProductsQueryResponse>
	{
		// Pagination MUST start from 1, NEVER 0.
		public int PageNumber { get; set; } = 1;

		// Safe default for page size
		public int PageSize { get; set; } = 10;
	}
}
