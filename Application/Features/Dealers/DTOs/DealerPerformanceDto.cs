namespace EvosancomAPI.Application.Features.Dealers.DTOs
{
	public class DealerPerformanceDto
	{
		public string DealerId { get; set; }
		public string CompanyName { get; set; }

		public decimal CurrentMonthSales { get; set; }
		public decimal LastMonthSales { get; set; }
		public decimal YearToDateSales { get; set; }
		public decimal AllTimeSales { get; set; }

		public int CurrentMonthOrders { get; set; }

		public decimal MonthlySalesQuota { get; set; }
		public decimal QuotaAchievementRate { get; set; }

		public decimal GrowthRate { get; set; }
		public decimal AverageOrderValue { get; set; }

		public List<MonthlyPerformanceDto> MonthlyPerformance { get; set; }
	}

}
