using EvosancomAPI.Application.Features.Dealers.DTOs;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Application.Repositories.Dealer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerPerformance
{
	public class GetDealerPerformanceQueryHandler
		: IRequestHandler<GetDealerPerformanceQueryRequest, GetDealerPerformanceQueryResponse>
	{ 
		

		public Task<GetDealerPerformanceQueryResponse> Handle(GetDealerPerformanceQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		/* 
private readonly IDealerReadRepository _dealerReadRepository;
private readonly IOrderReadRepository _orderReadRepository;
private readonly ILogger<GetDealerPerformanceQueryHandler> _logger;

public GetDealerPerformanceQueryHandler(
	IDealerReadRepository dealerReadRepository,
	IOrderReadRepository orderReadRepository,
	ILogger<GetDealerPerformanceQueryHandler> logger)
{
	_dealerReadRepository = dealerReadRepository;
	_orderReadRepository = orderReadRepository;
	_logger = logger;
}

public async Task<GetDealerPerformanceQueryResponse> Handle(
	GetDealerPerformanceQueryRequest request,
	CancellationToken cancellationToken)
{
	try
	{
		_logger.LogInformation("Fetching performance for dealer: {UserId}", request.UserId);

		// 1. Dealer bilgisini getir
		var dealer = await _dealerReadRepository.GetAll(false)
			.FirstOrDefaultAsync(
				d => d.UserId == request.UserId && !d.IsDeleted,
				cancellationToken);

		if (dealer == null)
		{
			return new GetDealerPerformanceQueryResponse
			{
				Success = false,
				Message = "Bayi bulunamadı."
			};
		}

		// 2. Tarih aralıkları
		var now = DateTime.UtcNow;
		var currentMonthStart = new DateTime(now.Year, now.Month, 1);
		var lastMonthStart = currentMonthStart.AddMonths(-1);
		var yearStart = new DateTime(now.Year, 1, 1);

		// 3. Bu ay siparişleri
		var currentMonthOrders = await _orderReadRepository.GetAll(false)
			.Where(o => o.UserId == request.UserId &&
						o.OrderDate >= currentMonthStart)
			.ToListAsync(cancellationToken);

		// 4. Geçen ay siparişleri
		var lastMonthOrders = await _orderReadRepository.GetAll(false)
			.Where(o => o.UserId == request.UserId &&
						o.OrderDate >= lastMonthStart &&
						o.OrderDate < currentMonthStart)
			.ToListAsync(cancellationToken);

		// 5. Bu yıl siparişleri
		var yearOrders = await _orderReadRepository.GetAll(false)
			.Where(o => o.UserId == request.UserId &&
						o.OrderDate >= yearStart)
			.ToListAsync(cancellationToken);

		// 6. Tüm zamanlar
		var allTimeOrders = await _orderReadRepository.GetAll(false)
			.Where(o => o.UserId == request.UserId)
			.ToListAsync(cancellationToken);

		// 7. Hesaplamalar
		decimal currentMonthSales = currentMonthOrders.Sum(o => o.FinalAmount);
		decimal lastMonthSales = lastMonthOrders.Sum(o => o.FinalAmount);
		decimal yearSales = yearOrders.Sum(o => o.FinalAmount);
		decimal allTimeSales = allTimeOrders.Sum(o => o.FinalAmount);

		// 8. Büyüme oranı (double formatında)
		double growthRate = lastMonthSales > 0
			? (double)((currentMonthSales - lastMonthSales) / lastMonthSales) * 100
			: 0;

		// 9. Ortalama sipariş değeri
		double averageOrderValue = currentMonthOrders.Any()
			? (double)currentMonthOrders.Average(o => o.FinalAmount)
			: 0;

		// 10. Aylık performans (son 12 ay)
		var monthlyPerformance = new List<MonthlyPerformanceDto>();

		for (int i = 11; i >= 0; i--)
		{
			var month = now.AddMonths(-i);
			var monthStart = new DateTime(month.Year, month.Month, 1);
			var monthEnd = monthStart.AddMonths(1).AddDays(-1);

			var monthOrders = await _orderReadRepository.GetAll(false)
				.Where(o => o.UserId == request.UserId &&
							o.OrderDate >= monthStart &&
							o.OrderDate <= monthEnd)
				.ToListAsync(cancellationToken);

			decimal totalSales = monthOrders.Sum(o => o.FinalAmount);

			monthlyPerformance.Add(new MonthlyPerformanceDto
			{
				Year = month.Year,
				Month = month.Month,
				MonthName = month.ToString("MMMM yyyy",
					new System.Globalization.CultureInfo("tr-TR")),
				TotalOrders = monthOrders.Count,
				TotalSales = totalSales,
				Quota = dealer.MonthlySalesQuota,
				QuotaMet = totalSales >= dealer.MonthlySalesQuota,
				QuotaPercentage = dealer.MonthlySalesQuota > 0
					? Math.Round(((double)totalSales / (double)dealer.MonthlySalesQuota) * 100, 2)
					: 0
			});
		}

		_logger.LogInformation("Performance data fetched successfully for dealer: {DealerId}", dealer.Id);

		return new GetDealerPerformanceQueryResponse
		{
			Success = true,
			Performance = new DealerPerformanceDto
			{
				DealerId = dealer.Id.ToString(),
				CompanyName = dealer.CompanyName,
				CurrentMonthSales = currentMonthSales,
				LastMonthSales = lastMonthSales,
				YearToDateSales = yearSales,
				AllTimeSales = allTimeSales,
				CurrentMonthOrders = currentMonthOrders.Count,
				MonthlySalesQuota = dealer.MonthlySalesQuota,

				QuotaAchievementRate = dealer.MonthlySalesQuota > 0
					? Math.Round(((double)currentMonthSales /
								  (double)dealer.MonthlySalesQuota) * 100, 2)
					: 0,

				GrowthRate = Math.Round(growthRate, 2),
				AverageOrderValue = Math.Round(averageOrderValue, 2),
				MonthlyPerformance = monthlyPerformance
			}
		};
	}
	catch (Exception ex)
	{
		_logger.LogError(ex, "Error occurred while fetching dealer performance");
		return new GetDealerPerformanceQueryResponse
		{
			Success = false,
			Message = "Performans verileri getirilirken bir hata oluştu: " + ex.Message
		};
	}
} */
	} 
}
