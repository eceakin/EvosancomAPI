using EvosancomAPI.Domain.Entities.Common;

namespace EvosancomAPI.Domain.Entities
{
	public class DealerSalesReport : BaseEntity
	{
		public Guid DealerId { get; set; }
		public int Year { get; set; }
		public int Month { get; set; }
		public decimal TotalSalesAmount { get; set; }
		public int TotalOrderCount { get; set; }
		public bool QuotaMet { get; set; } // Kota aşıldı mı?
		public DateTime ReportDate { get; set; }

		// Navigation Properties
		public Dealer Dealer { get; set; }
	}
}
