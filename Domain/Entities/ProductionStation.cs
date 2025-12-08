using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Domain.Entities
{
	// Domain/Entities/Production/ProductionStation.cs
	public class ProductionStation : BaseEntity
	{
		public Guid ProductionOrderId { get; set; }
		public int StationNumber { get; set; } // 1-5 arası
		public string StationName { get; set; } // Metal Kesimi, Boyama, vb.
		public StationStatus Status { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public string ProcessedByUserId { get; set; }
		public string BarcodeScanned { get; set; }
		public string Notes { get; set; }

		// Navigation Properties
		public ProductionOrder ProductionOrder { get; set; }
		public ApplicationUser ProcessedByUser { get; set; }
	}
}
