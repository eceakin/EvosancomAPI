using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.Dealer;
using EvosancomAPI.Application.Features.Dealers.Commands.CreateDealer;
using EvosancomAPI.Application.Repositories.Dealer;
using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Commands.UpdateDealer
{
	public class UpdateDealerCommandHandler : IRequestHandler<UpdateDealerCommandRequest, UpdateDealerCommandResponse>
	{
		readonly IDealerService _dealerService;

		public UpdateDealerCommandHandler(IDealerService dealerService)
		{
			_dealerService = dealerService;
		}

		public async Task<UpdateDealerCommandResponse> Handle(UpdateDealerCommandRequest request, CancellationToken cancellationToken)
		{
			await _dealerService.UpdateDealerAsync(new UpdateDealerDto
			{
				Id = request.Id,
				CompanyName = request.CompanyName,
				DiscountRate = request.DiscountRate,
				SalesQuota = request.SalesQuota
			});

			return new UpdateDealerCommandResponse { Succeeded = true, Message = "Bayi başarıyla güncellendi." };
		}
	
	}
}
