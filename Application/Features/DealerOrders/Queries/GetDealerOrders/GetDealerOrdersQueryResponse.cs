using EvosancomAPI.Application.Features.Orders.DTOs;
using EvosancomAPI.Domain.Entities.Common;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.GetDealerOrders
{
	public class GetDealerOrdersQueryResponse
	{
		public bool Success { get; set; }
		public string? Message { get; set; }
		public List<DealerOrderListDto> Orders { get; set; }
		public PaginationInfo? PaginationInfo { get; set; }
	}
}
