using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Domain.Entities
{
	public class ServiceRequest : BaseEntity
	{
		public string RequestNumber { get; set; }
		public string UserId { get; set; } // Müşteri
		public Guid? ProductId { get; set; }
		public string ProductBarcode { get; set; }
		public string ProblemDescription { get; set; }
		public ServiceRequestStatus Status { get; set; }
		public DateTime RequestDate { get; set; }
		public string AssignedToUserId { get; set; } // Teknik servis görevlisi
		public DateTime? PickupDate { get; set; }
		public DateTime? RepairStartDate { get; set; }
		public DateTime? RepairEndDate { get; set; }
		public DateTime? DeliveryDate { get; set; }
		public bool IsRepairable { get; set; }
		public string RepairNotes { get; set; }
		public decimal RepairCost { get; set; }
		public Guid? ReplacementOrderId { get; set; } // Onarılamazsa yeni sipariş

		// Navigation Properties
		public ApplicationUser User { get; set; }
		public Product Product { get; set; }
		public ApplicationUser AssignedToUser { get; set; }
		public Order ReplacementOrder { get; set; }
	}

}
