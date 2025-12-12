using EvosancomAPI.Application.Abstractions.Services;
using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Commands.DeleteDealer
{
	public class DeleteDealerCommandHandler : IRequestHandler<DeleteDealerCommandRequest, DeleteDealerCommandResponse>
	{
		readonly IDealerService _dealerService;

		public DeleteDealerCommandHandler(IDealerService dealerService)
		{
			_dealerService = dealerService;
		}

		public async Task<DeleteDealerCommandResponse> Handle(DeleteDealerCommandRequest request, CancellationToken cancellationToken)
		{
			await _dealerService.DeleteDealerAsync(request.Id);
			return new DeleteDealerCommandResponse { Success = true, Message = "Dealer başarıyla silindi." };
		}
	}
}
