using EvosancomAPI.Application.Abstractions.Hubs;
using EvosancomAPI.Application.Abstractions.Services;
using MediatR;

namespace EvosancomAPI.Application.Features.Orders.Commands.CreateOrder
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
	{
		readonly IOrderService _orderService;
		readonly IBasketService _basketService;
		readonly IOrderHubService _orderHubService;
		public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
		{
			_orderService = orderService;
			_basketService = basketService;
			_orderHubService = orderHubService;
		}

		public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
		{
			await _orderService.CreateOrderAsync(new()
			{
				FinalAmount = request.FinalAmount,
				DiscountAmount = request.DiscountAmount,
				ShippingAddress = request.ShippingAddress,
				OrderDate = request.OrderDate,
				Status = request.Status,
				TotalAmount = request.TotalAmount,
				BasketId = _basketService.GetUserActiveBasket?.Id.ToString()
			});
			await _orderHubService.OrderAddedMessageAsync("Yeni bir sipariş geldş");
			return new();
		}
	}
}
