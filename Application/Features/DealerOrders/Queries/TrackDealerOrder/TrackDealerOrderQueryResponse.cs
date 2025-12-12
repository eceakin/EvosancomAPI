using EvosancomAPI.Application.Features.Orders.DTOs;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.TrackDealerOrder
{
	public class TrackDealerOrderQueryResponse
	{
		public bool Success { get; set; }
		public string? Message { get; set; }

		public OrderTrackingDto? Tracking { get; set; }
	}
}
