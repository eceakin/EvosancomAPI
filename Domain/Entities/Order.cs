using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Domain.Entities
{
	public class Order : BaseEntity
	{
		public DateTime OrderDate { get; set; }
		public OrderStatus Status { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalAmount { get; set; }
		public string ShippingAddress { get; set; }
		public DateTime? EstimatedDeliveryDate { get; set; }
		public DateTime? ActualDeliveryDate { get; set; }


		public Basket Basket { get; set; }
		public ICollection<Product> Products { get; set; }
		// Navigation Properties
		//	public ICollection<OrderItem> OrderItems { get; set; }
	}
}