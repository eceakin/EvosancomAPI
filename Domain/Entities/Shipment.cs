using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Domain.Enums;
using EvosancomAPI.Domain.Entities.Identity;
namespace EvosancomAPI.Domain.Entities
{
	public class Shipment : BaseEntity
	{
		public Guid WarehouseEntryId { get; set; }
		public Guid OrderId { get; set; }
		public string ShipmentNumber { get; set; }
		public DateTime ShipmentDate { get; set; }
		public string VehiclePlate { get; set; }
		public string DriverName { get; set; }
		public string DriverPhone { get; set; }
		public string LoadedByUserId { get; set; }
		public string BarcodeScanned { get; set; }
		public ShipmentStatus Status { get; set; }
		public string TrackingNumber { get; set; }

		// Navigation Properties
		public WarehouseEntry WarehouseEntry { get; set; }
		public Order Order { get; set; }
		public ApplicationUser LoadedByUser { get; set; }
	}

}
