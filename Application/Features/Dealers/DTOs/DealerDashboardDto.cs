using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.DTOs
{
	public class DealerDashboardDto
	{
        public string DealerId { get; set; }
        public decimal DiscountRate { get; set; }
        public string CompanyName { get; set; }
        public decimal TotalOrdersThisMonth { get; set; }
		public decimal TotalSalesThisMonth { get; set; }
        public decimal MonthlySalesQuota { get; set; }
		public decimal QuotaPercentage { get; set; }
		public bool QuotaMet { get; set; }
		public int PendingOrders { get; set; }
		public int InProductionOrders { get; set; }
		public int CompletedOrders { get; set; }

		public List<MonthlySalesDto> Last6MonthsSales { get; set; }
		public List<RecentOrderDto> RecentOrders { get; set; }
	}
	/*
	 * DealerId = dealer.Id,
						CompanyName = dealer.CompanyName,
						DiscountRate = dealer.DiscountRate,
						TotalOrdersThisMonth = totalOrdersThisMonth,
						TotalSalesThisMonth = totalSalesThisMonth,
						MonthlySalesQuota = dealer.MonthlySalesQuota,
						QuotaPercentage = Math.Round(quotaPercentage, 2),
						QuotaMet = quotaMet,
						PendingOrders = pendingOrders,
						InProductionOrders = inProductionOrders,
						CompletedOrders = completedOrders,
						Last6MonthsSales = last6MonthsSales,
						RecentOrders = recentOrders */ 
}
