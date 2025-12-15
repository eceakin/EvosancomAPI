using EvosancomAPI.Domain.Entities.Common;

namespace EvosancomAPI.Domain.Entities
{
	public class OrderItem : BaseEntity
	{
		public Guid OrderId { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal TotalPrice { get; set; }
		public bool HasCustomDimensions { get; set; }

		// Navigation Properties
		public Order Order { get; set; }
		public Product Product { get; set; }
		//public CustomDimension CustomDimension { get; set; }
	}
}
