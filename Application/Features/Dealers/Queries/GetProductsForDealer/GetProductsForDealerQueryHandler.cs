using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.Dealer;
using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetProductsForDealer
{
	public class GetProductsForDealerQueryHandler : IRequestHandler<GetProductsForDealerQueryRequest, List<DealerProductListDto>>
	{
		private readonly IDealerService _dealerService;

		public GetProductsForDealerQueryHandler(IDealerService dealerService)
		{
			_dealerService = dealerService;
		}

		public async Task<List<DealerProductListDto>> Handle(GetProductsForDealerQueryRequest request, CancellationToken cancellationToken)
		{
			return await _dealerService.GetProductsForDealerAsync(request.UserId);
		}
	}
}
