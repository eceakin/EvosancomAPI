using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Domain.Entities
{
	public class DealerNotification : BaseEntity
	{
		public Guid DealerId { get; set; }
		public NotificationType Type { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public bool IsRead { get; set; }
		public DateTime? ReadDate { get; set; }
		public string ReferenceOrderId { get; set; }

		// Navigation
		public Dealer Dealer { get; set; }
	}
}
