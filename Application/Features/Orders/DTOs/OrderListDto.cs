using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Application.Features.Orders.DTOs
{
	public class OrderListDto
	{
		public Guid Id { get; set; }
		public string OrderNumber { get; set; } = string.Empty;
		public string UserFullName { get; set; } = string.Empty;
		public DateTime OrderDate { get; set; }
		public OrderStatus Status { get; set; }
		public string StatusText { get; set; } = string.Empty;
		public decimal FinalAmount { get; set; }
		public int ItemCount { get; set; }
	}

}
