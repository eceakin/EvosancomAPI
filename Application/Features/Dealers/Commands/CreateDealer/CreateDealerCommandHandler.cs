using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvosancomAPI.Application.Features.Dealers.Commands.CreateDealer
{
	public class CreateDealerCommandHandler : IRequestHandler<CreateDealerCommandRequest, CreateDealerCommandResponse>
	{
		private readonly IDealerWriteRepository _dealerWriteRepository;
		private readonly UserManager<ApplicationUser> _userManager;

		public CreateDealerCommandHandler(IDealerWriteRepository dealerWriteRepository, UserManager<ApplicationUser> userManager)
		{
			_dealerWriteRepository = dealerWriteRepository;
			_userManager = userManager;
		}

		public async Task<CreateDealerCommandResponse> Handle(CreateDealerCommandRequest request, CancellationToken cancellationToken)
		{
			var user = new ApplicationUser
			{
				UserName = request.Username,
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				DateOfBirth = request.DateOfBirth,
				CreatedDate = DateTime.UtcNow

			};
			var userResult = await _userManager.CreateAsync(user, request.Password);
			if (!userResult.Succeeded)
			{
				return new CreateDealerCommandResponse
				{
					Success = false,
					Message = "Kullanıcı oluşturulamadı: " + string.Join(", ", userResult.Errors.Select(e => e.Description))
				};
			}
			var dealer = new Dealer
			{
				UserId = user.Id,
				CompanyName = request.CompanyName,
				TaxNumber = request.TaxNumber,
				TaxOffice = request.TaxOffice,
				Phone = request.Phone,
				Address = request.Address,
				City = request.City,
				District = request.District,
				DiscountRate = request.DiscountRate,
				MonthlySalesQuota = request.MonthlySalesQuota,
				IsActive = true,
				ContractStartDate = request.ContractStartDate,
				ContractEndDate = request.ContractEndDate
			};
			await _dealerWriteRepository.AddAsync(dealer);
			await _dealerWriteRepository.SaveAsync();

			return new CreateDealerCommandResponse
			{
				Success = true,
				Message = "Bayi başarıyla oluşturuldu.",
				DealerId = dealer.Id,
				UserId = user.Id
			};
		}
	}
}
