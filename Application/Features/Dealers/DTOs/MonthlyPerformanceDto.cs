namespace EvosancomAPI.Application.Features.Dealers.DTOs
{
	public class MonthlyPerformanceDto
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public string MonthName { get; set; }

		public int TotalOrders { get; set; }
		public decimal TotalSales { get; set; }

		public decimal Quota { get; set; }
		public bool QuotaMet { get; set; }
		public decimal QuotaPercentage { get; set; }
	}

}
