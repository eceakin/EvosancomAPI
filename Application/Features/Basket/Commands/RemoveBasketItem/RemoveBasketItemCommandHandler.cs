using EvosancomAPI.Application.Abstractions.Services;
using MediatR;

namespace EvosancomAPI.Application.Features.Basket.Commands.RemoveBasketItem
{
	public class RemoveBasketItemCommandHandler :
		IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>
	{
		readonly IBasketService _basketService;

		public RemoveBasketItemCommandHandler(IBasketService basketService)
		{
			_basketService = basketService;
		}

		public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
		{
			await _basketService.DeleteItemAsync(request.BasketItemId);
			return new();
		}
	}
}
