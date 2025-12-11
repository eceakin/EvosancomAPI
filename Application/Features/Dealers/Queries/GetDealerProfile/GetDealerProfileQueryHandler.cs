using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerProfile
{
	public class GetDealerProfileQueryHandler : IRequestHandler<GetDealerProfileQueryRequest, GetDealerProfileQueryResponse>
	{
		public Task<GetDealerProfileQueryResponse> Handle(GetDealerProfileQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
