using EvosancomAPI.Application.DTOs.Order;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.Orders.Queries.GetOrderById
{
	public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
	{
		private readonly IOrderReadRepository _orderReadRepository;

		public GetOrderByIdQueryHandler(IOrderReadRepository orderReadRepository)
		{
			_orderReadRepository = orderReadRepository;
		}

		public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var order = await _orderReadRepository.Table
				.Include(o => o.Basket)
				.ThenInclude(b => b.BasketItems)
				.ThenInclude(bi => bi.Product)
				.FirstOrDefaultAsync(o => o.Id == Guid.Parse(request.Id), cancellationToken);

			if (order == null)
				throw new Exception("Sipariş bulunamadı.");

			return new GetOrderByIdQueryResponse
			{
				Id = order.Id,
				OrderDate = order.OrderDate,
				Status = GetOrderStatusText(order.Status),
				TotalAmount = order.TotalAmount,
				DiscountAmount = order.DiscountAmount,
				FinalAmount = order.FinalAmount,
				ShippingAddress = order.ShippingAddress,
				EstimatedDeliveryDate = order.EstimatedDeliveryDate,
				ActualDeliveryDate = order.ActualDeliveryDate,
				Items = order.Basket.BasketItems.Select(bi => new OrderItemDto
				{
					Id = bi.Id,
					ProductName = bi.Product.Name,
					Quantity = bi.Quantity,
					UnitPrice = bi.Product.BasePrice,
					TotalPrice = bi.Product.BasePrice * bi.Quantity
				}).ToList()
			};
		}

		private static string GetOrderStatusText(OrderStatus status)
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
