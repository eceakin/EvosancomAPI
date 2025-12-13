using EvosancomAPI.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EvosancomAPI.Application.Features.Basket.Queries.GetBasketItems
{
	public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
	{
		readonly IBasketService _basketService;
		public GetBasketItemsQueryHandler(IBasketService basketService)
		{
			_basketService = basketService;
		}

		public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
		{
			var basketItems = await _basketService
				.GetBasketItemsAsync();
			return basketItems.Select(ba => new GetBasketItemsQueryResponse
			{
				BasketItemId = ba.Id.ToString(),
				Name = ba.Product.Name,
				Price = ba.Product.BasePrice,
				Quantity = ba.Quantity
			}).ToList(); ;
		}
	}
}
