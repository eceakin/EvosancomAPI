using MediatR;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.GetDealerOrderById
{
	public class GetDealerOrderByIdQueryHandler : IRequestHandler<GetDealerOrderByIdQueryRequest, GetDealerOrderByIdQueryResponse>
	{
		public Task<GetDealerOrderByIdQueryResponse> Handle(GetDealerOrderByIdQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
