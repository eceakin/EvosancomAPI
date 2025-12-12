using AutoMapper;
using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetAllDealers
{
	public class GetAllDealersQueryHandler : IRequestHandler<GetAllDealersQueryRequest, GetAllDealersQueryResponse>
	{
		readonly IDealerService _dealerService;
		public GetAllDealersQueryHandler(IDealerService dealerService)
		{
			_dealerService = dealerService;
		}
		public async Task<GetAllDealersQueryResponse> Handle(GetAllDealersQueryRequest request, CancellationToken cancellationToken)
		{
			var dealers = await _dealerService.GetAllDealersAsync();
			return new GetAllDealersQueryResponse
			{
				Dealers = dealers
			};
		}
	}
}
