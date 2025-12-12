using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;

namespace EvosancomAPI.Domain.Entities
{
	public class Dealer : BaseEntity
	{
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		public string CompanyName { get; set; }

		// Bayi Özellikleri
		public decimal DiscountRate { get; set; } // Örn: 0.10 (%10)
		public decimal SalesQuota { get; set; } // Aylık/Yıllık Kota
		public decimal CurrentPeriodSales { get; set; } // Şu anki satış toplamı

		// Bayi Sözleşme Tarihi
		public DateTime ContractDate { get; set; }
	}
}
