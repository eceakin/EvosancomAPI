namespace EvosancomAPI.Application.Features.Dealers.DTOs
{
	public class DealerOrderItemDto
	{
		public Guid OrderItemId { get; set; }

		public Guid ProductId { get; set; }

		public string ProductName { get; set; }

		public string ProductCode { get; set; }

		public string CategoryName { get; set; }

		public int Quantity { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal TotalPrice { get; set; }

		public bool HasCustomDimensions { get; set; }

		public CustomDimensionDetailDto? CustomDimension { get; set; }
	}
}
