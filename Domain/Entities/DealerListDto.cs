namespace EvosancomAPI.Domain.Entities
{
	public class DealerListDto
	{
		public Guid Id { get; set; }
		public string CompanyName { get; set; }
		public string City { get; set; }
		public string Phone { get; set; }
		public decimal DiscountRate { get; set; }
		public bool IsActive { get; set; }
		public decimal TotalSalesThisMonth { get; set; }
		public bool QuotaMetThisMonth { get; set; }
	}
}
