namespace EvosancomAPI.Application.Features.Orders.DTOs
{
	public class CreateOrderItemDto
	{
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public bool HasCustomDimensions { get; set; }
		public CustomDimensionDto? CustomDimension { get; set; }
	}

}
