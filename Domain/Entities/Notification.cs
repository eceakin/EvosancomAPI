using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Domain.Entities
{
	public class Notification : BaseEntity
	{
		public string UserId { get; set; }
		public NotificationType Type { get; set; } // SMS, Email, Push
		public string Title { get; set; }
		public string Message { get; set; }
		public bool IsSent { get; set; }
		public DateTime? SentDate { get; set; }
		public bool IsRead { get; set; }
		public DateTime? ReadDate { get; set; }
		public string ReferenceId { get; set; } // Sipariş no, vb.

		// Navigation Properties
		public ApplicationUser User { get; set; }
	}

}
