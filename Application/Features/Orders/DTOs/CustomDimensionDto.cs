namespace EvosancomAPI.Application.Features.Orders.DTOs
{
	public class CustomDimensionDto
	{
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Depth { get; set; }
		public decimal AdditionalCost { get; set; }
		public string? Notes { get; set; }
	}
}
