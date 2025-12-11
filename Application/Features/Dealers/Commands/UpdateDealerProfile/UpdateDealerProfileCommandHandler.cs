using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Commands.UpdateDealerProfile
{
	public class UpdateDealerProfileCommandHandler : IRequestHandler<UpdateDealerProfileCommandRequest, UpdateDealerProfileCommandResponse>
	{
		public Task<UpdateDealerProfileCommandResponse> Handle(UpdateDealerProfileCommandRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
