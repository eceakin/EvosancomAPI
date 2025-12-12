using EvosancomAPI.Application.Features.Dealers.DTOs;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.GetDealerOrderById
{
	public class GetDealerOrderByIdQueryResponse
	{
		public bool Success { get; set; }
		public string? Message { get; set; }

		public DealerOrderDetailDto? Order { get; set; }
	}
}
