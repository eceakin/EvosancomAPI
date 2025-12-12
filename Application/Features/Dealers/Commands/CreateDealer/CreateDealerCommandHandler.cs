using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.Dealer;
using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Commands.CreateDealer
{
	public class CreateDealerCommandHandler : IRequestHandler<CreateDealerCommandRequest, CreateDealerCommandResponse>
	{
		private readonly IDealerService _dealerService;

		public CreateDealerCommandHandler(IDealerService dealerService)
		{
			_dealerService = dealerService;
		}

		public async Task<CreateDealerCommandResponse> Handle(CreateDealerCommandRequest request, CancellationToken cancellationToken)
		{
			await _dealerService.CreateDealerAsync(new CreateDealerDto
			{
				UserId = request.UserId,
				CompanyName = request.CompanyName,
				DiscountRate = request.DiscountRate,
				SalesQuota = request.SalesQuota
			});

			return new CreateDealerCommandResponse { Succeeded = true, Message = "Bayi başarıyla oluşturuldu." };
		}
	}
}