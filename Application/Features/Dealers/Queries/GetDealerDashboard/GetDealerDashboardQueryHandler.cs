using EvosancomAPI.Application.Features.Dealers.DTOs;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerDashboard
{
public class GetDealerDashboardQueryHandler : IRequestHandler<GetDealerDashboardQueryRequest, GetDealerDashboardQueryResponse>
	{
		private readonly IDealerReadRepository _dealerReadRepository;
		private readonly IOrderReadRepository _orderReadRepository;
		private readonly ILogger<GetDealerDashboardQueryHandler> _logger;

		public GetDealerDashboardQueryHandler(
			IDealerReadRepository dealerReadRepository,
			IOrderReadRepository orderReadRepository,
			ILogger<GetDealerDashboardQueryHandler> logger)
		{
			_dealerReadRepository = dealerReadRepository;
			_orderReadRepository = orderReadRepository;
			_logger = logger;
		}

		public async Task<GetDealerDashboardQueryResponse> Handle(
			GetDealerDashboardQueryRequest request, 
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Fetching dashboard for dealer: {UserId}", request.UserId);

				// 1. Dealer bilgisini getir
				var dealer = await _dealerReadRepository.GetAll(false)
					.Include(d => d.User)
					.FirstOrDefaultAsync(d => d.UserId == request.UserId && !d.IsDeleted, cancellationToken);

				if (dealer == null)
				{
					_logger.LogWarning("Dealer not found with UserId: {UserId}", request.UserId);
					return new GetDealerDashboardQueryResponse
					{
						Success = false,
						Message = "Bayi bulunamadı."
					};
				}

				// 2. Bu ayki tarih aralığı
				var now = DateTime.UtcNow;
				var monthStart = new DateTime(now.Year, now.Month, 1);
				var monthEnd = monthStart.AddMonths(1).AddDays(-1);

				// 3. Bu ayki siparişleri getir
				var ordersQuery = _orderReadRepository.GetAll(false)
					.Where(o => o.UserId == request.UserId && 
					           o.OrderDate >= monthStart && 
					           o.OrderDate <= monthEnd);

				var thisMonthOrders = await ordersQuery.ToListAsync(cancellationToken);

				// 4. İstatistikleri hesapla
				var totalOrdersThisMonth = thisMonthOrders.Count;
				var totalSalesThisMonth = thisMonthOrders.Sum(o => o.FinalAmount);
				
				var pendingOrders = thisMonthOrders.Count(o => 
					o.Status == OrderStatus.Pending || o.Status == OrderStatus.Confirmed);
				
				var inProductionOrders = thisMonthOrders.Count(o => 
					o.Status == OrderStatus.InProduction || 
					o.Status == OrderStatus.QualityControl);
				
				var completedOrders = thisMonthOrders.Count(o => 
					o.Status == OrderStatus.Delivered);

				// 5. Kota hesaplama
				var quotaPercentage = dealer.MonthlySalesQuota > 0 
					? (totalSalesThisMonth / dealer.MonthlySalesQuota) * 100 
					: 0;

				var quotaMet = totalSalesThisMonth >= dealer.MonthlySalesQuota;

				// 6. Son 6 ay satış verileri
				var last6MonthsSales = new List<MonthlySalesDto>();
				for (int i = 5; i >= 0; i--)
				{
					var month = now.AddMonths(-i);
					var monthStartDate = new DateTime(month.Year, month.Month, 1);
					var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

					var monthlySales = await _orderReadRepository.GetAll(false)
						.Where(o => o.UserId == request.UserId && 
						           o.OrderDate >= monthStartDate && 
						           o.OrderDate <= monthEndDate)
						.SumAsync(o => o.FinalAmount, cancellationToken);

					last6MonthsSales.Add(new MonthlySalesDto
					{
						Year = month.Year,
						Month = month.Month,
						MonthName = month.ToString("MMMM", new System.Globalization.CultureInfo("tr-TR")),
						TotalSales = monthlySales
					});
				}

				// 7. Son siparişler (en son 5)
				var recentOrders = await _orderReadRepository.GetAll(false)
					.Where(o => o.UserId == request.UserId)
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
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Dashboard data fetched successfully for dealer: {DealerId}", dealer.Id);

				return new GetDealerDashboardQueryResponse
				{
					Success = true,
					Dashboard = new DealerDashboardDto
					{
						DealerId = dealer.Id.ToString(),
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
						RecentOrders = recentOrders
					}
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching dealer dashboard");
				return new GetDealerDashboardQueryResponse
				{
					Success = false,
					Message = "Dashboard verileri getirilirken bir hata oluştu: " + ex.Message
				};
			}
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
