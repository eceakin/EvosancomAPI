namespace EvosancomAPI.Application.Features.PriceCalculation.DTOs
{
	public class PriceBreakdownDto
	{
		public decimal BasePrice { get; set; }
		public decimal CustomDimensionCost { get; set; }
		public decimal Subtotal { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal TotalPrice { get; set; }
		public List<PriceDetailDto> Details { get; set; } = new();
	}
}
