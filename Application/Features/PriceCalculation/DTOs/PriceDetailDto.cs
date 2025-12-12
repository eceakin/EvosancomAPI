namespace EvosancomAPI.Application.Features.PriceCalculation.DTOs
{
	public class PriceDetailDto
	{
		public string Label { get; set; }
		public decimal Amount { get; set; }
		public bool IsDiscount { get; set; }
		public string Description { get; set; }
	}
}