namespace EvosancomAPI.Application.DTOs.Dealer
{
	public class UpdateDealerDto
	{
		public string Id { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal SalesQuota { get; set; }
		public string CompanyName { get; set; }
	}
}