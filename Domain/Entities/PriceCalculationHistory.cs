using EvosancomAPI.Domain.Entities.Common;

namespace EvosancomAPI.Domain.Entities
{
	public class PriceCalculationHistory : BaseEntity
	{
		public Guid DealerId { get; set; }
		public Guid ProductId { get; set; }
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Depth { get; set; }
		public decimal BasePrice { get; set; }
		public decimal CustomDimensionCost { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal DiscountedPrice { get; set; }
		public bool ConvertedToOrder { get; set; }
		public Guid? OrderId { get; set; }

		// Navigation
		public Dealer Dealer { get; set; }
		public Product Product { get; set; }
		public Order Order { get; set; }
	}
}
