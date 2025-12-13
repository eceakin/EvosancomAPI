using EvosancomAPI.Application.DTOs.BasketItem;
using EvosancomAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Abstractions.Services
{
	public interface IBasketService
	{
		public Task<List<BasketItem>> GetBasketItemsAsync();
		public Task AddItemToBasketAsync(CreateBasketItemDto basketItem	);
		public Task UpdateQuantityAsync(UpdateBasketItemDto basketItem);
		public Task DeleteItemAsync(string basketItemId);

	}
}
