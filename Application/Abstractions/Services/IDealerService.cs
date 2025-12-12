using EvosancomAPI.Application.Features.Dealers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Abstractions.Services
{
	public interface IDealerService
	{
		/// <summary>
		/// Bayinin dashboard verilerini getirir
		/// </summary>
		Task<DealerDashboardDto> GetDealerDashboardAsync(string userId);

		/// <summary>
		/// Bayinin performans verilerini getirir
		/// </summary>
		Task<DealerPerformanceDto> GetDealerPerformanceAsync(string userId);

		/// <summary>
		/// Bayinin iskonto oranını hesaplar
		/// </summary>
		Task<decimal> CalculateDealerDiscountAsync(Guid dealerId, decimal basePrice);

		/// <summary>
		/// Bayinin aylık kota durumunu kontrol eder
		/// </summary>
		Task<bool> CheckQuotaStatusAsync(Guid dealerId);

		/// <summary>
		/// Bayinin bu ayki toplam satışını getirir
		/// </summary>
		Task<decimal> GetMonthlyTotalSalesAsync(Guid dealerId);

		/// <summary>
		/// Bayinin bu ayki sipariş sayısını getirir
		/// </summary>
		Task<int> GetMonthlyOrderCountAsync(Guid dealerId);

		/// <summary>
		/// Bayi aktif mi kontrol eder
		/// </summary>
		Task<bool> IsDealerActiveAsync(Guid dealerId);

		/// <summary>
		/// UserId'den DealerId'yi bulur
		/// </summary>
		Task<Guid?> GetDealerIdByUserIdAsync(string userId);

		/// <summary>
		/// Bayinin iskonto oranını getirir
		/// </summary>
		Task<decimal> GetDealerDiscountRateAsync(Guid dealerId);

		/// <summary>
		/// Bayinin aylık satış kotasını getirir
		/// </summary>
		Task<decimal> GetMonthlySalesQuotaAsync(Guid dealerId);
	}

}
