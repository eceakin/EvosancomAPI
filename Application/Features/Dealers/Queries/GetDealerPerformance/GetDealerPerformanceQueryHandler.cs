using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerPerformance
{
	public class GetDealerPerformanceQueryHandler : IRequestHandler<GetDealerPerformanceQueryRequest, GetDealerPerformanceQueryResponse>
	{
		public Task<GetDealerPerformanceQueryResponse> Handle(GetDealerPerformanceQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}

}
