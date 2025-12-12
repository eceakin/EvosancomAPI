using EvosancomAPI.Application.Features.Orders.DTOs;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.GetDealerOrders
{
	public class GetDealerOrdersQueryHandler : IRequestHandler<GetDealerOrdersQueryRequest, GetDealerOrdersQueryResponse>
	{
		private readonly IOrderReadRepository _orderReadRepository;
		private readonly ILogger<GetDealerOrdersQueryHandler> _logger;

		public GetDealerOrdersQueryHandler(
			IOrderReadRepository orderReadRepository,
			ILogger<GetDealerOrdersQueryHandler> logger)
		{
			_orderReadRepository = orderReadRepository;
			_logger = logger;
		}

		public async Task<GetDealerOrdersQueryResponse> Handle(
			GetDealerOrdersQueryRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Fetching orders for dealer: {UserId}", request.UserId);

				// 1. Base query
				var query = _orderReadRepository.GetAll(false)
					.Where(o => o.UserId == request.UserId);

				// 2. Status filtresi
				if (request.Status.HasValue)
				{
					query = query.Where(o => o.Status == request.Status.Value);
				}

				// 3. Tarih filtresi
				if (request.StartDate.HasValue)
				{
					query = query.Where(o => o.OrderDate >= request.StartDate.Value);
				}

				if (request.EndDate.HasValue)
				{
					query = query.Where(o => o.OrderDate <= request.EndDate.Value);
				}

				// 4. Arama
				if (!string.IsNullOrWhiteSpace(request.SearchTerm))
				{
					query = query.Where(o =>
						o.OrderNumber.Contains(request.SearchTerm) ||
						o.Notes.Contains(request.SearchTerm));
				}

				// 5. Sıralama

				query = request.OrderBy?.ToLower() switch
				{
					"date_asc" => query.OrderBy(o => o.OrderDate),
					"date_desc" => query.OrderByDescending(o => o.OrderDate),
					"amount_asc" => query.OrderBy(o => o.FinalAmount),
					"amount_desc" => query.OrderByDescending(o => o.FinalAmount),
					_ => query.OrderByDescending(o => o.OrderDate)
				};

				// 6. Toplam kayıt sayısı
				var totalCount = await query.CountAsync(cancellationToken);

				// 7. Pagination
				var orders = await query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.Include(o => o.OrderItems)
					.Select(o => new DealerOrderListDto
					{
						OrderId = o.Id.ToString(),
						OrderNumber = o.OrderNumber,
						OrderDate = o.OrderDate,
						Status = o.Status,
						StatusText = GetOrderStatusText(o.Status),
						TotalAmount = o.TotalAmount,
						DiscountAmount = o.DiscountAmount,
						FinalAmount = o.FinalAmount,
						ItemCount = o.OrderItems.Count,
						EstimatedDeliveryDate = o.EstimatedDeliveryDate,
						ActualDeliveryDate = o.ActualDeliveryDate
					})
					.ToListAsync(cancellationToken);

				_logger.LogInformation("Found {Count} orders for dealer", totalCount);

				return new GetDealerOrdersQueryResponse
				{
					Success = true,
					Orders = orders,
					PaginationInfo = new PaginationInfo
					{
						CurrentPage = request.PageNumber,
						PageSize = request.PageSize,
						TotalCount = totalCount,
						TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
					}
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching dealer orders");
				return new GetDealerOrdersQueryResponse
				{
					Success = false,
					Message = "Siparişler getirilirken bir hata oluştu: " + ex.Message
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
