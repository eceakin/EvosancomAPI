using EvosancomAPI.Application.Features.Orders.DTOs;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.TrackDealerOrder
{


	public class TrackDealerOrderQueryHandler : IRequestHandler<TrackDealerOrderQueryRequest, TrackDealerOrderQueryResponse>
	{
		private readonly IOrderReadRepository _orderReadRepository;
		private readonly IProductionOrderReadRepository _productionOrderReadRepository;
		private readonly ILogger<TrackDealerOrderQueryHandler> _logger;

		public TrackDealerOrderQueryHandler(
			IOrderReadRepository orderReadRepository,
			IProductionOrderReadRepository productionOrderReadRepository,
			ILogger<TrackDealerOrderQueryHandler> logger)
		{
			_orderReadRepository = orderReadRepository;
			_productionOrderReadRepository = productionOrderReadRepository;
			_logger = logger;
		}

		public async Task<TrackDealerOrderQueryResponse> Handle(
			TrackDealerOrderQueryRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Tracking order {OrderId} for dealer {UserId}",
					request.OrderId, request.UserId);

				// 1. Siparişi getir
				var order = await _orderReadRepository.GetAll(false)
					.Include(o => o.StatusHistories.OrderBy(sh => sh.ChangedDate))
					.Include(o => o.OrderItems)
					.FirstOrDefaultAsync(o => o.Id == request.OrderId && o.UserId == request.UserId,
						cancellationToken);

				if (order == null)
				{
					return new TrackDealerOrderQueryResponse
					{
						Success = false,
						Message = "Sipariş bulunamadı."
					};
				}

				// 2. Üretim bilgilerini getir (eğer üretimdeyse)
				ProductionTrackingDto productionTracking = null;
				if (order.Status == OrderStatus.InProduction || order.Status == OrderStatus.QualityControl)
				{
					var productionOrders = await _productionOrderReadRepository.GetAll(false)
						.Include(po => po.ProductionStations.OrderBy(ps => ps.StationNumber))
						.Where(po => po.OrderId == order.Id)
						.ToListAsync(cancellationToken);

					if (productionOrders.Any())
					{
						// İlk production order'ı al (basitleştirme için)
						var productionOrder = productionOrders.First();

						productionTracking = new ProductionTrackingDto
						{
							ProductionOrderId = productionOrder.Id,
							ProductionNumber = productionOrder.ProductionNumber,
							Status = productionOrder.Status,
							CurrentStationNumber = productionOrder.CurrentStationId,
							CurrentStationName = GetStationName(productionOrder.CurrentStationId),
							CompletedStations = productionOrder.ProductionStations
								.Count(ps => ps.Status == StationStatus.Completed),
							TotalStations = 5,
							ProductionStations = productionOrder.ProductionStations.Select(ps => new ProductionStationDto
							{
								StationNumber = ps.StationNumber,
								StationName = ps.StationName,
								Status = ps.Status,
								StatusText = GetStationStatusText(ps.Status),
								StartTime = ps.StartTime,
								EndTime = ps.EndTime
							}).ToList(),
							EstimatedCompletionDate = productionOrder.EstimatedCompletionDate,
							ActualCompletionDate = productionOrder.ActualCompletionDate
						};
					}
				}

				// 3. Tracking bilgisini oluştur
				var tracking = new OrderTrackingDto
				{
					OrderId = order.Id,
					OrderNumber = order.OrderNumber,
					OrderDate = order.OrderDate,
					CurrentStatus = order.Status,
					CurrentStatusText = GetOrderStatusText(order.Status),
					EstimatedDeliveryDate = order.EstimatedDeliveryDate,
					ActualDeliveryDate = order.ActualDeliveryDate,
					ProgressPercentage = CalculateProgressPercentage(order.Status, productionTracking),
					StatusHistory = order.StatusHistories.Select(sh => new OrderStatusHistoryDto
					{
						Status = sh.Status,
						StatusText = GetOrderStatusText(sh.Status),
						ChangedDate = sh.ChangedDate,
						Notes = sh.Notes,
						NotificationSent = sh.NotificationSent
					}).ToList(),
					ProductionTracking = productionTracking
				};

				return new TrackDealerOrderQueryResponse
				{
					Success = true,
					Tracking = tracking
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while tracking order");
				return new TrackDealerOrderQueryResponse
				{
					Success = false,
					Message = "Sipariş takibi yapılırken bir hata oluştu: " + ex.Message
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

		private string GetStationName(int stationNumber)
		{
			return stationNumber switch
			{
				1 => "Metal Kesimi ve Delme",
				2 => "Boyama ve Kaplama",
				3 => "İzolasyon",
				4 => "Montaj",
				5 => "Son Testler",
				_ => $"İstasyon {stationNumber}"
			};
		}

		private string GetStationStatusText(StationStatus status)
		{
			return status switch
			{
				StationStatus.Pending => "Beklemede",
				StationStatus.InProgress => "İşlemde",
				StationStatus.Completed => "Tamamlandı",
				StationStatus.Failed => "Başarısız",
				_ => status.ToString()
			};
		}

		private int CalculateProgressPercentage(OrderStatus status, ProductionTrackingDto productionTracking)
		{
			return status switch
			{
				OrderStatus.Pending => 0,
				OrderStatus.Confirmed => 10,
				OrderStatus.InProduction => productionTracking != null
					? 20 + (productionTracking.CompletedStations * 10)
					: 20,
				OrderStatus.QualityControl => 70,
				OrderStatus.InWarehouse => 80,
				OrderStatus.Shipped => 90,
				OrderStatus.Delivered => 100,
				OrderStatus.Cancelled => 0,
				_ => 0
			};
		}
	}
}
