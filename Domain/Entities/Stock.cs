using EvosancomAPI.Domain.Entities.Common;

namespace EvosancomAPI.Domain.Entities
{
	public class Stock : BaseEntity
	{
		public Guid ProductId { get; set; }
		public int AvailableQuantity { get; set; }
		public int ReservedQuantity { get; set; }
		public int InProductionQuantity { get; set; }
		public int MinimumStockLevel { get; set; }
		public DateTime LastUpdated { get; set; }

		// Navigation Properties
		public Product Product { get; set; }
		public ICollection<StockMovement> StockMovements { get; set; }
	}

}
