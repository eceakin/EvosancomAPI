using MediatR;

namespace EvosancomAPI.Application.Features.DealerProducts.Queries.GetDealerProducts
{
	public class GetDealerProductsQueryHandler:IRequestHandler<GetDealerProductsQueryRequest, GetDealerProductsQueryResponse>
	{
		public Task<GetDealerProductsQueryResponse> Handle(GetDealerProductsQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
	{
	}
}
