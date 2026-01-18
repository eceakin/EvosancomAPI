namespace EvosancomAPI.Application.Features.Orders.Commands.CreateOrder
{
	public class CreateOrderCommandResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public Guid OrderId { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalAmount { get; set; }
	}
}
