using EvosancomAPI.Application.Features.Dealers.DTOs;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Application.Features.Orders.DTOs
{
	public class OrderTrackingDto
	{
		public Guid OrderId { get; set; }

		public string OrderNumber { get; set; }

		public DateTime OrderDate { get; set; }

		public OrderStatus CurrentStatus { get; set; }

		public string CurrentStatusText { get; set; }

		public DateTime? EstimatedDeliveryDate { get; set; }

		public DateTime? ActualDeliveryDate { get; set; }

		public int ProgressPercentage { get; set; }

		public List<OrderStatusHistoryDto> StatusHistory { get; set; }

		public ProductionTrackingDto? ProductionTracking { get; set; }
	}
}
