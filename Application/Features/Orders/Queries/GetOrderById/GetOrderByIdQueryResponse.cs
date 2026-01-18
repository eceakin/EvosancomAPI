using EvosancomAPI.Application.DTOs.Order;

namespace EvosancomAPI.Application.Features.Orders.Queries.GetOrderById
{
	public class GetOrderByIdQueryResponse
	{
		public Guid Id { get; set; }
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalAmount { get; set; }
		public string ShippingAddress { get; set; }
		public DateTime? EstimatedDeliveryDate { get; set; }
		public DateTime? ActualDeliveryDate { get; set; }
		public List<OrderItemDto> Items { get; set; }
	}
}
