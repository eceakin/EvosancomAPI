using EvosancomAPI.Application.Features.Orders.DTOs;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Application.Features.Orders.DTOs
{
	public class DealerOrderListDto
	{
		public string OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public string OrderNumber { get; set; }
		public string StatusText { get; set; }
        public OrderStatus Status { get; set; }
        public OrderStatus OrderStatus { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalAmount { get; set; }
		public int ItemCount { get; set; }
		public DateTime? EstimatedDeliveryDate { get; set; }
		public DateTime? ActualDeliveryDate
		{
			get; set;


		}
	}
}