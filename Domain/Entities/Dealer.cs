using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;

namespace EvosancomAPI.Domain.Entities
{
	public class Dealer : BaseEntity
	{
		public string UserId { get; set; } // Identity User Id (Bayi hesabı)
		public string CompanyName { get; set; }
		public string TaxNumber { get; set; }
		public string TaxOffice { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public decimal DiscountRate { get; set; } // Bayi iskonto oranı
		public decimal MonthlySalesQuota { get; set; } // Aylık satış kotası
		public bool IsActive { get; set; }
		public DateTime ContractStartDate { get; set; }
		public DateTime? ContractEndDate { get; set; }

		// Navigation Properties
		public ApplicationUser User { get; set; }
		public ICollection<DealerSalesReport> SalesReports { get; set; }
	}
}
