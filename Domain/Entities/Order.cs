using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Domain.Entities
{
	public class Order : BaseEntity
	{
		public string OrderNumber { get; set; }
		public string UserId { get; set; } // Identity User Id
		public DateTime OrderDate { get; set; }
		public OrderStatus Status { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalAmount { get; set; }
		public string ShippingAddress { get; set; }
		public string BillingAddress { get; set; }
		public string Notes { get; set; }
		public DateTime? EstimatedDeliveryDate { get; set; }
		public DateTime? ActualDeliveryDate { get; set; }

		// Navigation Properties
		public ApplicationUser User { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
		public ICollection<OrderStatusHistory> StatusHistories { get; set; }
	}
}