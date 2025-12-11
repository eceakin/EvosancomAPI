using MediatR;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.GetDealerOrders
{
	public class GetDealerOrdersQueryHandler :IRequestHandler<GetDealerOrdersQueryRequest, GetDealerOrdersQueryResponse>
	{
		public Task<GetDealerOrdersQueryResponse> Handle(GetDealerOrdersQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
	{
	}
}
