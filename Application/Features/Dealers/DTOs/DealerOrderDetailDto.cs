using EvosancomAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.DTOs
{
	public class DealerOrderDetailDto
	{
		public Guid OrderId { get; set; }

		public string OrderNumber { get; set; }

		public DateTime OrderDate { get; set; }

		public OrderStatus Status { get; set; }

		public string StatusText { get; set; }

		public decimal TotalAmount { get; set; }

		public decimal DiscountAmount { get; set; }

		public decimal FinalAmount { get; set; }

		public string ShippingAddress { get; set; }

		public string BillingAddress { get; set; }

		public string Notes { get; set; }

		public DateTime? EstimatedDeliveryDate { get; set; }

		public DateTime? ActualDeliveryDate { get; set; }

		public List<DealerOrderItemDto> OrderItems { get; set; }

		public List<OrderStatusHistoryDto> StatusHistory { get; set; }
	}
}
