using MediatR;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.TrackDealerOrder
{


	public class TrackDealerOrderQueryHandler : IRequestHandler<TrackDealerOrderQueryRequest, TrackDealerOrderQueryResponse>
	{
		public Task<TrackDealerOrderQueryResponse> Handle(TrackDealerOrderQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
