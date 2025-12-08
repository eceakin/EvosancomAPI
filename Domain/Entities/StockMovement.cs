using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Domain.Entities
{
	// Domain/Entities/Inventory/StockMovement.cs
	public class StockMovement : BaseEntity
	{
		public Guid StockId { get; set; }
		public MovementType Type { get; set; } // Giriş, Çıkış, Transfer
		public int Quantity { get; set; }
		public string Reason { get; set; }
		public DateTime MovementDate { get; set; }
		public string ProcessedByUserId { get; set; }
		public string ReferenceNumber { get; set; } // Sipariş no, üretim no, vb.

		// Navigation Properties
		public Stock Stock { get; set; }
		public ApplicationUser ProcessedByUser { get; set; }
	}

}
