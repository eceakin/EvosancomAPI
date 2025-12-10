using EvosancomAPI.Application.Features.Dealers.DTOs;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerById
{
	public class GetDealerByIdQueryHandler : IRequestHandler<GetDealerByIdQueryRequest, GetDealerByIdQueryResponse>
	{
		private readonly IDealerReadRepository _dealerReadRepository;

		public GetDealerByIdQueryHandler(IDealerReadRepository dealerReadRepository)
		{
			_dealerReadRepository = dealerReadRepository;
		}

		public async Task<GetDealerByIdQueryResponse> Handle(GetDealerByIdQueryRequest request, CancellationToken cancellationToken)
		{
			if (!Guid.TryParse(request.Id, out Guid dealerId))
			{
				// Eğer geçerli bir Guid formatı değilse direkt hata dön
				return new GetDealerByIdQueryResponse
				{
					Success = false,
					Message = "Geçersiz ID formatı."
				};
			}
			var dealer = await _dealerReadRepository.GetAll(false)
				.Include(d => d.User)
				.FirstOrDefaultAsync(d => d.Id == dealerId && !d.IsDeleted, cancellationToken);


			if (dealer == null)
			{
				return new GetDealerByIdQueryResponse
				{
					Success = false,
					Message = "Bayi bulunamadı."
				};
			}

			return new GetDealerByIdQueryResponse
			{
				Success = true,
				Dealer = new DealerDto
				{
					Id = dealer.Id,
					UserId = dealer.UserId,
					UserFullName = $"{dealer.User.FirstName} {dealer.User.LastName}",
					Email = dealer.User.Email,
					CompanyName = dealer.CompanyName,
					Address = dealer.Address,
					City = dealer.City,
					DiscountRate = dealer.DiscountRate,
					MonthlySalesQuota = dealer.MonthlySalesQuota,
					IsActive = dealer.IsActive,
					ContractStartDate = dealer.ContractStartDate,
					ContractEndDate = dealer.ContractEndDate,
					CreatedDate = dealer.CreatedDate
				}
			};

		}
	}
}
