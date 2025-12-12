namespace EvosancomAPI.Application.Features.DealerOrders.Commands.CreateDealerOrder
{
	public class CreateDealerOrderCommandResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public string OrderId { get; set; }
		public string OrderNumber { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalAmount { get; set; }
		public DateTime EstimatedDeliveryDate { get; set; }


	}
}
