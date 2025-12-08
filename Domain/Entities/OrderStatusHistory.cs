using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Domain.Entities
{
	public class OrderStatusHistory : BaseEntity
	{
		public Guid OrderId { get; set; }
		public OrderStatus Status { get; set; }
		public DateTime ChangedDate { get; set; }
		public string ChangedByUserId { get; set; }
		public string Notes { get; set; }
		public bool NotificationSent { get; set; } // SMS/Email gönderildi mi?

		// Navigation Properties
		public Order Order { get; set; }
		public ApplicationUser ChangedByUser { get; set; }
	}
}
