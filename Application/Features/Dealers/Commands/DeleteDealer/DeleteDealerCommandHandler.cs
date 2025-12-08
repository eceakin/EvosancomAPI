using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Commands.DeleteDealer
{
	public class DeleteDealerCommandHandler : IRequestHandler<DeleteDealerCommandRequest, DeleteDealerCommandResponse>
	{
		readonly IDealerWriteRepository _dealerWriteRepository;

		public DeleteDealerCommandHandler(IDealerWriteRepository dealerWriteRepository)
		{
			_dealerWriteRepository = dealerWriteRepository;
		}

		public async Task<DeleteDealerCommandResponse> Handle(DeleteDealerCommandRequest request, CancellationToken cancellationToken)
		{
			await _dealerWriteRepository.RemoveAsync(request.Id);
			await _dealerWriteRepository.SaveAsync();
			return new()
			{
				Message = "Dealer deleted successfully."

			};
		}
	}
}
