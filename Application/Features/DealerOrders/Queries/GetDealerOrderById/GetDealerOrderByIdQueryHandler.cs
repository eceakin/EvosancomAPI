using EvosancomAPI.Application.Features.Dealers.DTOs;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.GetDealerOrderById
{
	public class GetDealerOrderByIdQueryHandler : IRequestHandler<GetDealerOrderByIdQueryRequest, GetDealerOrderByIdQueryResponse>
	{
		private readonly IOrderReadRepository _orderReadRepository;
		private readonly ILogger<GetDealerOrderByIdQueryHandler> _logger;

		public GetDealerOrderByIdQueryHandler(
			IOrderReadRepository orderReadRepository,
			ILogger<GetDealerOrderByIdQueryHandler> logger)
		{
			_orderReadRepository = orderReadRepository;
			_logger = logger;
		}

		public async Task<GetDealerOrderByIdQueryResponse> Handle(
			GetDealerOrderByIdQueryRequest request,
			CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Fetching order {OrderId} for dealer {UserId}",
					request.OrderId, request.UserId);

				var order = await _orderReadRepository.GetAll(false)
					.Include(o => o.OrderItems)
						.ThenInclude(oi => oi.Product)
							.ThenInclude(p => p.ProductCategory)
					.Include(o => o.OrderItems)
						.ThenInclude(oi => oi.CustomDimension)
					.Include(o => o.StatusHistories.OrderBy(sh => sh.ChangedDate))
					.Include(o => o.User)
					.FirstOrDefaultAsync(o => o.Id == request.OrderId && o.UserId == request.UserId,
						cancellationToken);

				if (order == null)
				{
					_logger.LogWarning("Order not found: {OrderId}", request.OrderId);
					return new GetDealerOrderByIdQueryResponse
					{
						Success = false,
						Message = "Sipariş bulunamadı."
					};
				}

				var orderDto = new DealerOrderDetailDto
				{
					OrderId = order.Id,
					OrderNumber = order.OrderNumber,
					OrderDate = order.OrderDate,
					Status = order.Status,
					StatusText = GetOrderStatusText(order.Status),
					TotalAmount = order.TotalAmount,
					DiscountAmount = order.DiscountAmount,
					FinalAmount = order.FinalAmount,
					ShippingAddress = order.ShippingAddress,
					BillingAddress = order.BillingAddress,
					Notes = order.Notes,
					EstimatedDeliveryDate = order.EstimatedDeliveryDate,
					ActualDeliveryDate = order.ActualDeliveryDate,
					OrderItems = order.OrderItems.Select(oi => new DealerOrderItemDto
					{
						OrderItemId = oi.Id,
						ProductId = oi.ProductId,
						ProductName = oi.Product.Name,
						ProductCode = oi.Product.Code,
						CategoryName = oi.Product.ProductCategory.Name,
						Quantity = oi.Quantity,
						UnitPrice = oi.UnitPrice,
						TotalPrice = oi.TotalPrice,
						HasCustomDimensions = oi.HasCustomDimensions,
						CustomDimension = oi.CustomDimension != null ? new CustomDimensionDetailDto
						{
							Width = oi.CustomDimension.Width,
							Height = oi.CustomDimension.Height,
							Depth = oi.CustomDimension.Depth,
							AdditionalCost = oi.CustomDimension.AdditionalCost
						} : null
					}).ToList(),
					StatusHistory = order.StatusHistories.Select(sh => new OrderStatusHistoryDto
					{
						Status = sh.Status,
						StatusText = GetOrderStatusText(sh.Status),
						ChangedDate = sh.ChangedDate,
						Notes = sh.Notes,
						NotificationSent = sh.NotificationSent
					}).ToList()
				};

				return new GetDealerOrderByIdQueryResponse
				{
					Success = true,
					Order = orderDto
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error occurred while fetching order by id");
				return new GetDealerOrderByIdQueryResponse
				{
					Success = false,
					Message = "Sipariş detayları getirilirken bir hata oluştu: " + ex.Message
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
