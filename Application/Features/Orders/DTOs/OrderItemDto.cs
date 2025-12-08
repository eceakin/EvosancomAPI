namespace EvosancomAPI.Application.Features.Orders.DTOs
{
	public class OrderItemDto
	{
		public Guid Id { get; set; }
		public Guid ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal TotalPrice { get; set; }
		public bool HasCustomDimensions { get; set; }
		public CustomDimensionDto? CustomDimension { get; set; }
	}

}
