using EvosancomAPI.Application.Repositories.Dealer;
using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Commands.UpdateDealer
{
	public class UpdateDealerCommandHandler : IRequestHandler<UpdateDealerCommandRequest, UpdateDealerCommandResponse>
	{
		private readonly IDealerWriteRepository _dealerWriteRepository;
		private readonly IDealerReadRepository _dealerReadRepository;

		public UpdateDealerCommandHandler(IDealerReadRepository dealerReadRepository, IDealerWriteRepository dealerWriteRepository)
		{
			_dealerReadRepository = dealerReadRepository;
			_dealerWriteRepository = dealerWriteRepository;
		}

		public async Task<UpdateDealerCommandResponse> Handle(UpdateDealerCommandRequest request, CancellationToken cancellationToken)
		{
			var dealer = await _dealerReadRepository.GetByIdAsync(request.Id.ToString());
			if (dealer == null)
			{
				return new UpdateDealerCommandResponse
				{
					Success = false,
					Message = "Dealer not found."
				};
			}
			dealer.CompanyName = request.CompanyName;
			dealer.Phone = request.Phone;
			dealer.Address = request.Address;
			dealer.City = request.City;
			dealer.District = request.District;
			dealer.DiscountRate = request.DiscountRate;
			dealer.MonthlySalesQuota = request.MonthlySalesQuota;
			dealer.IsActive = request.IsActive;
			dealer.ContractEndDate = request.ContractEndDate?.ToUniversalTime();
			dealer.UpdatedDate = DateTime.UtcNow;

			 _dealerWriteRepository.UpdateAsync(dealer);
			await _dealerWriteRepository.SaveAsync();
			return new UpdateDealerCommandResponse
			{
				Success = true,
				Message = "Dealer updated successfully."
			};


		}
	}
}
