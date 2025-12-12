using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Application.Features.Dealers.DTOs
{
	public class OrderStatusHistoryDto
	{
		public OrderStatus Status { get; set; }

		public string StatusText { get; set; }

		public DateTime ChangedDate { get; set; }

		public string Notes { get; set; }

		public bool NotificationSent { get; set; }
	}
}
