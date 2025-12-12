using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvosancomAPI.Application.Features.Dealers.Commands.UpdateDealerProfile
{

	public class UpdateDealerProfileCommandHandler : IRequestHandler<UpdateDealerProfileCommandRequest, UpdateDealerProfileCommandResponse>
	{
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IDealerWriteRepository _dealerWriteRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<UpdateDealerProfileCommandHandler> _logger;

		public UpdateDealerProfileCommandHandler(
			IDealerReadRepository dealerReadRepository,
			IDealerWriteRepository dealerWriteRepository,
			UserManager<ApplicationUser> userManager,
			ILogger<UpdateDealerProfileCommandHandler> logger)
		{
			_dealerReadRepository = dealerReadRepository;
			_dealerWriteRepository = dealerWriteRepository;
			_userManager = userManager;
			_logger = logger;
		}

		public async Task<UpdateDealerProfileCommandResponse> Handle(
			UpdateDealerProfileCommandRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Updating profile for dealer: {UserId}", request.UserId);

				// 1. Dealer'ı getir
				var dealer = await _dealerReadRepository.GetAll(true)
					.Include(d => d.User)
					.FirstOrDefaultAsync(d => d.UserId == request.UserId && !d.IsDeleted, cancellationToken);

				if (dealer == null)
				{
					_logger.LogWarning("Dealer not found with UserId: {UserId}", request.UserId);
					return new UpdateDealerProfileCommandResponse
					{
						Success = false,
						Message = "Bayi bulunamadı."
					};
				}

				// 2. Email değişikliği kontrolü
				if (!string.IsNullOrEmpty(request.Email) && request.Email != dealer.User.Email)
				{
					var existingUser = await _userManager.FindByEmailAsync(request.Email);
					if (existingUser != null && existingUser.Id != dealer.UserId)
					{
						return new UpdateDealerProfileCommandResponse
						{
							Success = false,
							Message = "Bu email adresi başka bir kullanıcı tarafından kullanılıyor."
						};
					}
					dealer.User.Email = request.Email;
				}

				// 3. Telefon numarası değişikliği kontrolü
				if (!string.IsNullOrEmpty(request.Phone) && request.Phone != dealer.Phone)
				{
					var existingDealer = await _dealerReadRepository
						.GetSingleAsync(d => d.Phone == request.Phone && d.Id != dealer.Id && !d.IsDeleted, false);

					if (existingDealer != null)
					{
						return new UpdateDealerProfileCommandResponse
						{
							Success = false,
							Message = "Bu telefon numarası başka bir bayi tarafından kullanılıyor."
						};
					}
					dealer.Phone = request.Phone;
					dealer.User.PhoneNumber = request.Phone;
				}

				// 4. Kullanıcı bilgilerini güncelle
				if (!string.IsNullOrEmpty(request.FirstName))
					dealer.User.FirstName = request.FirstName;

				if (!string.IsNullOrEmpty(request.LastName))
					dealer.User.LastName = request.LastName;

				var userUpdateResult = await _userManager.UpdateAsync(dealer.User);
				if (!userUpdateResult.Succeeded)
				{
					_logger.LogError("User update failed: {Errors}",
						string.Join(", ", userUpdateResult.Errors.Select(e => e.Description)));
					return new UpdateDealerProfileCommandResponse
					{
						Success = false,
						Message = "Kullanıcı bilgileri güncellenemedi."
					};
				}

				// 5. Dealer bilgilerini güncelle
				if (!string.IsNullOrEmpty(request.CompanyName))
					dealer.CompanyName = request.CompanyName;

				if (!string.IsNullOrEmpty(request.Address))
					dealer.Address = request.Address;

				if (!string.IsNullOrEmpty(request.City))
					dealer.City = request.City;

				if (!string.IsNullOrEmpty(request.District))
					dealer.District = request.District;

				dealer.UpdatedDate = DateTime.UtcNow;

				_dealerWriteRepository.UpdateAsync(dealer);
				await _dealerWriteRepository.SaveAsync();

				_logger.LogInformation("Dealer profile updated successfully. DealerId: {DealerId}", dealer.Id);

				return new UpdateDealerProfileCommandResponse
				{
					Success = true,
					Message = "Profil bilgileri başarıyla güncellendi."
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while updating dealer profile");
				return new UpdateDealerProfileCommandResponse
				{
					Success = false,
					Message = "Profil güncellenirken bir hata oluştu: " + ex.Message
				};
			}
		}
	}
}
