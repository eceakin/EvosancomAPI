using EvosancomAPI.Application.Abstractions.Hubs;
using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.Order;
using MediatR;

namespace EvosancomAPI.Application.Features.Orders.Commands.CreateOrder
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
	{
		private readonly IOrderService _orderService;
		private readonly IBasketService _basketService;
		private readonly IOrderHubService _orderHubService;

		public CreateOrderCommandHandler(
			IOrderService orderService,
			IBasketService basketService,
			IOrderHubService orderHubService)
		{
			_orderService = orderService;
			_basketService = basketService;
			_orderHubService = orderHubService;
		}

		public async Task<CreateOrderCommandResponse> Handle(
			CreateOrderCommandRequest request,
			CancellationToken cancellationToken)
		{
			// Get the user's active basket
			var basket = _basketService.GetUserActiveBasket;

			if (basket == null)
				throw new Exception("Aktif sepet bulunamadı.");

			// Create the order with automatic calculations
			var result = await _orderService.CreateOrderAsync(new CreateOrderDto
			{
				BasketId = basket.Id.ToString(),
				ShippingAddress = request.ShippingAddress,
				EstimatedDeliveryDate = request.EstimatedDeliveryDate
			});

			// Send notification
			await _orderHubService.OrderAddedMessageAsync(
				$"Yeni sipariş alındı. Sipariş No: {result.OrderId}");

			return new CreateOrderCommandResponse
			{
				Success = true,
				Message = "Sipariş başarıyla oluşturuldu.",
				OrderId = result.OrderId,
				TotalAmount = result.TotalAmount,
				DiscountAmount = result.DiscountAmount,
				FinalAmount = result.FinalAmount
			};
		}
	}
}