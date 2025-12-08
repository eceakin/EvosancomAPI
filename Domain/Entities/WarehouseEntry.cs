using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;
namespace EvosancomAPI.Domain.Entities
{
	public class WarehouseEntry : BaseEntity
	{
		public Guid ProductionOrderId { get; set; }
		public DateTime EntryDate { get; set; }
		public DateTime EstimatedShipmentDate { get; set; }
		public DateTime? ActualShipmentDate { get; set; }
		public string ReceivedByUserId { get; set; }
		public string BarcodeScanned { get; set; }
		public WarehouseStatus Status { get; set; }
		public string StorageLocation { get; set; }

		// Navigation Properties
		public ProductionOrder ProductionOrder { get; set; }
		public ApplicationUser ReceivedByUser { get; set; }
		public Shipment Shipment { get; set; }
	}
}
