using EvosancomAPI.Application.Features.Dealers.DTOs;
using EvosancomAPI.Application.Repositories.Dealer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerProfile
{
	public class GetDealerProfileQueryHandler : IRequestHandler<GetDealerProfileQueryRequest, GetDealerProfileQueryResponse>
	{
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly ILogger<GetDealerProfileQueryHandler> _logger;

		public GetDealerProfileQueryHandler(
			IDealerReadRepository dealerReadRepository,
			ILogger<GetDealerProfileQueryHandler> logger)
		{
			_dealerReadRepository = dealerReadRepository;
			_logger = logger;
		}

		public async Task<GetDealerProfileQueryResponse> Handle(
			GetDealerProfileQueryRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Fetching profile for dealer: {UserId}", request.UserId);

				var dealer = await _dealerReadRepository.GetAll(false)
					.Include(d => d.User)
					.FirstOrDefaultAsync(d => d.UserId == request.UserId && !d.IsDeleted, cancellationToken);

				if (dealer == null)
				{
					_logger.LogWarning("Dealer not found with UserId: {UserId}", request.UserId);
					return new GetDealerProfileQueryResponse
					{
						Success = false,
						Message = "Bayi bulunamadı."
					};
				}

				return new GetDealerProfileQueryResponse
				{
					Success = true,
					Profile = new DealerDto
					{
						Id = dealer.Id,
						UserId = dealer.UserId,
						Username = dealer.User.UserName,
						Email = dealer.User.Email,
						FirstName = dealer.User.FirstName,
						LastName = dealer.User.LastName,
						CompanyName = dealer.CompanyName,
						TaxNumber = dealer.TaxNumber,
						TaxOffice = dealer.TaxOffice,
						Phone = dealer.Phone,
						Address = dealer.Address,
						City = dealer.City,
						District = dealer.District,
						DiscountRate = dealer.DiscountRate,
						MonthlySalesQuota = dealer.MonthlySalesQuota,
						IsActive = dealer.IsActive,
						ContractStartDate = dealer.ContractStartDate,
						ContractEndDate = dealer.ContractEndDate,
						CreatedDate = dealer.CreatedDate
					}
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching dealer profile");
				return new GetDealerProfileQueryResponse
				{
					Success = false,
					Message = "Profil bilgileri getirilirken bir hata oluştu: " + ex.Message
				};
			}
		}
	}
}
