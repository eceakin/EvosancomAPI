using EvosancomAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Orders.DTOs
{

	public class OrderDto
	{
		public Guid Id { get; set; }
		public string OrderNumber { get; set; } = string.Empty;
		public string UserId { get; set; } = string.Empty;
		public string UserFullName { get; set; } = string.Empty;
		public DateTime OrderDate { get; set; }
		public OrderStatus Status { get; set; }
		public string StatusText { get; set; } = string.Empty;
		public decimal TotalAmount { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalAmount { get; set; }
		public string? ShippingAddress { get; set; }
		public string? BillingAddress { get; set; }
		public string? Notes { get; set; }
		public DateTime? EstimatedDeliveryDate { get; set; }
		public DateTime? ActualDeliveryDate { get; set; }
		public List<OrderItemDto> OrderItems { get; set; } = new();
	}

}
