using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EvosancomAPI.Application.Features.Dealers.Commands.CreateDealer
{
	public class CreateDealerCommandHandler : IRequestHandler<CreateDealerCommandRequest, CreateDealerCommandResponse>
	{
		private readonly IDealerWriteRepository _dealerWriteRepository;
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly ILogger<CreateDealerCommandHandler> _logger;

		public CreateDealerCommandHandler(
			IDealerWriteRepository dealerWriteRepository,
			IDealerReadRepository dealerReadRepository,
			UserManager<ApplicationUser> userManager,
			RoleManager<ApplicationRole> roleManager,
			ILogger<CreateDealerCommandHandler> logger)
		{
			_dealerWriteRepository = dealerWriteRepository;
			_dealerReadRepository = dealerReadRepository;
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
		}

		public async Task<CreateDealerCommandResponse> Handle(CreateDealerCommandRequest request, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Creating new dealer with email: {Email}", request.Email);

				// 1. Email kontrolü
				var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
				if (existingUserByEmail != null)
				{
					_logger.LogWarning("Email already exists: {Email}", request.Email);
					return new CreateDealerCommandResponse
					{
						Success = false,
						Message = "Bu email adresi zaten kullanılıyor."
					};
				}

				// 2. Username kontrolü
				var existingUserByUsername = await _userManager.FindByNameAsync(request.Username);
				if (existingUserByUsername != null)
				{
					_logger.LogWarning("Username already exists: {Username}", request.Username);
					return new CreateDealerCommandResponse
					{
						Success = false,
						Message = "Bu kullanıcı adı zaten kullanılıyor."
					};
				}

				// 3. Vergi numarası kontrolü
				var existingDealer = await _dealerReadRepository
					.GetSingleAsync(d => d.TaxNumber == request.TaxNumber && !d.IsDeleted, tracking: false);

				if (existingDealer != null)
				{
					_logger.LogWarning("Tax number already exists: {TaxNumber}", request.TaxNumber);
					return new CreateDealerCommandResponse
					{
						Success = false,
						Message = "Bu vergi numarası ile kayıtlı bir bayi zaten mevcut."
					};
				}

				// 4. Bayi rolü kontrolü
				if (!await _roleManager.RoleExistsAsync("Bayi"))
				{
					_logger.LogInformation("Creating 'Bayi' role as it doesn't exist");
					await _roleManager.CreateAsync(new ApplicationRole
					{
						Id = Guid.NewGuid().ToString(),
						Name = "Bayi",
						NormalizedName = "BAYI"
					});
				}

				// 5. Kullanıcı oluştur
				var user = new ApplicationUser
				{
					Id = Guid.NewGuid().ToString(),
					UserName = request.Username,
					Email = request.Email,
					EmailConfirmed = true,
					FirstName = request.FirstName,
					LastName = request.LastName,
					DateOfBirth = request.DateOfBirth,
					CreatedDate = DateTime.UtcNow,
					PhoneNumber = request.Phone,
					PhoneNumberConfirmed = true
				};

				var userResult = await _userManager.CreateAsync(user, request.Password);

				if (!userResult.Succeeded)
				{
					_logger.LogError("User creation failed: {Errors}",
						string.Join(", ", userResult.Errors.Select(e => e.Description)));

					return new CreateDealerCommandResponse
					{
						Success = false,
						Message = "Kullanıcı oluşturulamadı: " +
							string.Join(", ", userResult.Errors.Select(e => e.Description))
					};
				}

				// 6. Kullanıcıya Bayi rolü ata
				await _userManager.AddToRoleAsync(user, "Bayi");
				_logger.LogInformation("User {Username} created and assigned to Bayi role", user.UserName);

				// 7. Dealer entity oluştur
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
					ContractStartDate = request.ContractStartDate.ToUniversalTime(),
					ContractEndDate = request.ContractEndDate?.ToUniversalTime(),
					CreatedDate = DateTime.UtcNow
				};

				await _dealerWriteRepository.AddAsync(dealer);
				await _dealerWriteRepository.SaveAsync();

				_logger.LogInformation("Dealer created successfully. DealerId: {DealerId}, UserId: {UserId}",
					dealer.Id, user.Id);

				return new CreateDealerCommandResponse
				{
					Success = true,
					Message = "Bayi başarıyla oluşturuldu.",
					DealerId = dealer.Id,
					UserId = user.Id
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while creating dealer");
				return new CreateDealerCommandResponse
				{
					Success = false,
					Message = "Bayi oluşturulurken bir hata oluştu: " + ex.Message
				};
			}
		}
	}
}
