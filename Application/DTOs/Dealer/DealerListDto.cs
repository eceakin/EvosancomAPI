namespace EvosancomAPI.Application.DTOs.Dealer
{
	public class DealerListDto
	{
		public string Id { get; set; }
		public string UserId { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal SalesQuota { get; set; }
		public string CompanyName { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}