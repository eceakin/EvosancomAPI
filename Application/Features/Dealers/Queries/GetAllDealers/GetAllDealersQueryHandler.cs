using AutoMapper;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetAllDealers
{
	public class GetAllDealersQueryHandler : IRequestHandler<GetAllDealersQueryRequest, GetAllDealersQueryResponse>
	{
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IMapper _mapper;
		public async Task<GetAllDealersQueryResponse> Handle(GetAllDealersQueryRequest request, CancellationToken cancellationToken)
		{
			var query = _dealerReadRepository.GetAll(false)
				.Include(d => d.User)
				.Where(d => !d.IsDeleted);
			if (request.IsActive.HasValue)
			{
				query = query.Where(d => d.IsActive == request.IsActive.Value);
			}
			if (!string.IsNullOrWhiteSpace(request.SearchTerm))
			{
				query = query.Where(d =>
					d.CompanyName.Contains(request.SearchTerm) ||
					d.User.FirstName.Contains(request.SearchTerm) ||
					d.User.LastName.Contains(request.SearchTerm));
			}
			var totalCount = await query.CountAsync(cancellationToken);
			var dealers = await query
			   .Skip((request.PageNumber - 1) * request.PageSize)
			   .Take(request.PageSize)
			   .Select(d => new DealerListDto
			   {
				   Id = d.Id,
				   CompanyName = d.CompanyName,
				   City = d.City,
				   Phone = d.Phone,
				   DiscountRate = d.DiscountRate,
				   IsActive = d.IsActive,
				   TotalSalesThisMonth = 0, // Bu bilgiyi SalesReport'tan alacağız
				   QuotaMetThisMonth = false
			   })
			   .ToListAsync(cancellationToken);

			return new GetAllDealersQueryResponse
			{
				Dealers = dealers,
				TotalDealerCount = totalCount,

			};
		}
	}
}
