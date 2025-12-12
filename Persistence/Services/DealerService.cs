using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.Features.Dealers.DTOs;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Persistence.Services
{
	public class DealerService : IDealerService
	{
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IOrderReadRepository _orderReadRepository;
		private readonly ILogger<DealerService> _logger;

		public DealerService(
			IDealerReadRepository dealerReadRepository,
			IOrderReadRepository orderReadRepository,
			ILogger<DealerService> logger)
		{
			_dealerReadRepository = dealerReadRepository;
			_orderReadRepository = orderReadRepository;
			_logger = logger;
		}

		public async Task<DealerDashboardDto> GetDealerDashboardAsync(string userId)
		{
			try
			{
				var dealer = await _dealerReadRepository.GetAll(false)
					.Include(d => d.User)
					.FirstOrDefaultAsync(d => d.UserId == userId && !d.IsDeleted);

				if (dealer == null)
					throw new Exception("Bayi bulunamadı.");

				var now = DateTime.UtcNow;
				var monthStart = new DateTime(now.Year, now.Month, 1);

				var thisMonthOrders = await _orderReadRepository.GetAll(false)
					.Where(o => o.UserId == userId && o.OrderDate >= monthStart)
					.ToListAsync();

				var totalSalesThisMonth = thisMonthOrders.Sum(o => o.FinalAmount);
				var quotaPercentage = dealer.MonthlySalesQuota > 0
					? (totalSalesThisMonth / dealer.MonthlySalesQuota) * 100
					: 0;

				// Son 6 ay satış verileri
				var last6MonthsSales = new List<MonthlySalesDto>();
				for (int i = 5; i >= 0; i--)
				{
					var month = now.AddMonths(-i);
					var monthStartDate = new DateTime(month.Year, month.Month, 1);
					var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

					var monthlySales = await _orderReadRepository.GetAll(false)
						.Where(o => o.UserId == userId &&
								   o.OrderDate >= monthStartDate &&
								   o.OrderDate <= monthEndDate)
						.SumAsync(o => o.FinalAmount);

					last6MonthsSales.Add(new MonthlySalesDto
					{
						Year = month.Year,
						Month = month.Month,
						MonthName = month.ToString("MMMM", new System.Globalization.CultureInfo("tr-TR")),
						TotalSales = monthlySales
					});
				}

				// Son siparişler
				var recentOrders = await _orderReadRepository.GetAll(false)
					.Where(o => o.UserId == userId)
					.OrderByDescending(o => o.OrderDate)
					.Take(5)
					.Select(o => new RecentOrderDto
					{
						Id = o.Id.ToString(),
						OrderNumber = o.OrderNumber,
						OrderDate = o.OrderDate,
						Status = o.Status,
						StatusText = GetOrderStatusText(o.Status),
						FinalAmount = o.FinalAmount
					})
					.ToListAsync();

				return new DealerDashboardDto
				{
					DealerId = dealer.Id.ToString(),
					CompanyName = dealer.CompanyName,
					DiscountRate = dealer.DiscountRate,
					TotalOrdersThisMonth = thisMonthOrders.Count,
					TotalSalesThisMonth = totalSalesThisMonth,
					MonthlySalesQuota = dealer.MonthlySalesQuota,
					QuotaPercentage = Math.Round(quotaPercentage, 2),
					QuotaMet = totalSalesThisMonth >= dealer.MonthlySalesQuota,
					PendingOrders = thisMonthOrders.Count(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Confirmed),
					InProductionOrders = thisMonthOrders.Count(o => o.Status == OrderStatus.InProduction || o.Status == OrderStatus.QualityControl),
					CompletedOrders = thisMonthOrders.Count(o => o.Status == OrderStatus.Delivered),
					Last6MonthsSales = last6MonthsSales,
					RecentOrders = recentOrders
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in GetDealerDashboardAsync");
				throw;
			}
		}

		public async Task<DealerPerformanceDto> GetDealerPerformanceAsync(string userId)
		{
			try
			{
				var dealer = await _dealerReadRepository.GetAll(false)
					.FirstOrDefaultAsync(d => d.UserId == userId && !d.IsDeleted);

				if (dealer == null)
					throw new Exception("Bayi bulunamadı.");

				var now = DateTime.UtcNow;
				var currentMonthStart = new DateTime(now.Year, now.Month, 1);
				var lastMonthStart = currentMonthStart.AddMonths(-1);
				var yearStart = new DateTime(now.Year, 1, 1);

				var currentMonthOrders = await _orderReadRepository.GetAll(false)
					.Where(o => o.UserId == userId && o.OrderDate >= currentMonthStart)
					.ToListAsync();

				var lastMonthOrders = await _orderReadRepository.GetAll(false)
					.Where(o => o.UserId == userId &&
							   o.OrderDate >= lastMonthStart &&
							   o.OrderDate < currentMonthStart)
					.ToListAsync();

				var yearOrders = await _orderReadRepository.GetAll(false)
					.Where(o => o.UserId == userId && o.OrderDate >= yearStart)
					.ToListAsync();

				var allTimeOrders = await _orderReadRepository.GetAll(false)
					.Where(o => o.UserId == userId)
					.ToListAsync();

				var currentMonthSales = currentMonthOrders.Sum(o => o.FinalAmount);
				var lastMonthSales = lastMonthOrders.Sum(o => o.FinalAmount);
				var growthRate = lastMonthSales > 0
					? ((currentMonthSales - lastMonthSales) / lastMonthSales) * 100
					: 0;

				// Aylık performans (son 12 ay)
				var monthlyPerformance = new List<MonthlyPerformanceDto>();
				for (int i = 11; i >= 0; i--)
				{
					var month = now.AddMonths(-i);
					var monthStart = new DateTime(month.Year, month.Month, 1);
					var monthEnd = monthStart.AddMonths(1).AddDays(-1);

					var monthOrders = await _orderReadRepository.GetAll(false)
						.Where(o => o.UserId == userId &&
								   o.OrderDate >= monthStart &&
								   o.OrderDate <= monthEnd)
						.ToListAsync();

					var totalSales = monthOrders.Sum(o => o.FinalAmount);

					monthlyPerformance.Add(new MonthlyPerformanceDto
					{
						Year = month.Year,
						Month = month.Month,
						MonthName = month.ToString("MMMM yyyy", new System.Globalization.CultureInfo("tr-TR")),
						TotalOrders = monthOrders.Count,
						TotalSales = totalSales,
						Quota = dealer.MonthlySalesQuota,
						QuotaMet = totalSales >= dealer.MonthlySalesQuota,
						QuotaPercentage = dealer.MonthlySalesQuota > 0
							? Math.Round((totalSales / dealer.MonthlySalesQuota) * 100, 2)
							: 0
					});
				}

				return new DealerPerformanceDto
				{
					DealerId = dealer.Id.ToString(),
					CompanyName = dealer.CompanyName,
					CurrentMonthSales = currentMonthSales,
					LastMonthSales = lastMonthSales,
					YearToDateSales = yearOrders.Sum(o => o.FinalAmount),
					AllTimeSales = allTimeOrders.Sum(o => o.FinalAmount),
					CurrentMonthOrders = currentMonthOrders.Count,
					MonthlySalesQuota = dealer.MonthlySalesQuota,
					QuotaAchievementRate = dealer.MonthlySalesQuota > 0
						? Math.Round((currentMonthSales / dealer.MonthlySalesQuota) * 100, 2)
						: 0,
					GrowthRate = Math.Round(growthRate, 2),
					AverageOrderValue = currentMonthOrders.Any()
						? Math.Round(currentMonthOrders.Average(o => o.FinalAmount), 2)
						: 0,
					MonthlyPerformance = monthlyPerformance
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in GetDealerPerformanceAsync");
				throw;
			}
		}

		public async Task<decimal> CalculateDealerDiscountAsync(Guid dealerId, decimal basePrice)
		{
			var dealer = await _dealerReadRepository.GetByIdAsync(dealerId.ToString(), false);
			if (dealer == null)
				throw new Exception("Bayi bulunamadı.");

			var discountAmount = basePrice * (dealer.DiscountRate / 100);
			return basePrice - discountAmount;
		}

		public async Task<bool> CheckQuotaStatusAsync(Guid dealerId)
		{
			var dealer = await _dealerReadRepository.GetByIdAsync(dealerId.ToString(), false);
			if (dealer == null)
				return false;

			var monthlySales = await GetMonthlyTotalSalesAsync(dealerId);
			return monthlySales >= dealer.MonthlySalesQuota;
		}

		public async Task<decimal> GetMonthlyTotalSalesAsync(Guid dealerId)
		{
			var dealer = await _dealerReadRepository.GetByIdAsync(dealerId.ToString(), false);
			if (dealer == null)
				return 0;

			var now = DateTime.UtcNow;
			var monthStart = new DateTime(now.Year, now.Month, 1);

			return await _orderReadRepository.GetAll(false)
				.Where(o => o.UserId == dealer.UserId && o.OrderDate >= monthStart)
				.SumAsync(o => o.FinalAmount);
		}

		public async Task<int> GetMonthlyOrderCountAsync(Guid dealerId)
		{
			var dealer = await _dealerReadRepository.GetByIdAsync(dealerId.ToString(), false);
			if (dealer == null)
				return 0;

			var now = DateTime.UtcNow;
			var monthStart = new DateTime(now.Year, now.Month, 1);

			return await _orderReadRepository.GetAll(false)
				.Where(o => o.UserId == dealer.UserId && o.OrderDate >= monthStart)
				.CountAsync();
		}

		public async Task<bool> IsDealerActiveAsync(Guid dealerId)
		{
			var dealer = await _dealerReadRepository.GetByIdAsync(dealerId.ToString(), false);
			return dealer != null && dealer.IsActive && !dealer.IsDeleted;
		}

		public async Task<Guid?> GetDealerIdByUserIdAsync(string userId)
		{
			var dealer = await _dealerReadRepository.GetAll(false)
				.FirstOrDefaultAsync(d => d.UserId == userId && !d.IsDeleted);

			return dealer?.Id;
		}

		public async Task<decimal> GetDealerDiscountRateAsync(Guid dealerId)
		{
			var dealer = await _dealerReadRepository.GetByIdAsync(dealerId.ToString(), false);
			return dealer?.DiscountRate ?? 0;
		}

		public async Task<decimal> GetMonthlySalesQuotaAsync(Guid dealerId)
		{
			var dealer = await _dealerReadRepository.GetByIdAsync(dealerId.ToString(), false);
			return dealer?.MonthlySalesQuota ?? 0;
		}

		private string GetOrderStatusText(OrderStatus status)
		{
			return status switch
			{
				OrderStatus.Pending => "Beklemede",
				OrderStatus.Confirmed => "Onaylandı",
				OrderStatus.InProduction => "Üretimde",
				OrderStatus.QualityControl => "Kalite Kontrolde",
				OrderStatus.InWarehouse => "Depoda",
				OrderStatus.Shipped => "Kargoda",
				OrderStatus.Delivered => "Teslim Edildi",
				OrderStatus.Cancelled => "İptal Edildi",
				_ => status.ToString()
			};
		}
	}
}