using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.BasketItem;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Application.Repositories.Basket;
using EvosancomAPI.Application.Repositories.BasketItems;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Services
{

	public class BasketService : IBasketService
	{
		readonly IHttpContextAccessor _httpContextAccessor;
		readonly UserManager<ApplicationUser> _userManager;
		readonly IBasketReadRepository _basketReadRepository;
		readonly IOrderReadRepository _orderReadRepository;
		readonly IBasketWriteRepository _basketWriteRepository;
		readonly IBasketItemWriteRepository _basketItemWriteRepository;
		readonly IBasketItemReadRepository _basketItemReadRepository;

		public Basket GetUserActiveBasket{
			get{
				Basket? basket =  ContextUser().Result;
				return basket;
			}}

		public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
		{
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
			_orderReadRepository = orderReadRepository;
			_basketWriteRepository = basketWriteRepository;
			_basketItemWriteRepository = basketItemWriteRepository;
			_basketItemReadRepository = basketItemReadRepository;
			_basketReadRepository = basketReadRepository;
		}
		private async Task<Basket?> ContextUser()
		{
			var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
			if (!string.IsNullOrEmpty(username))
			{
				ApplicationUser? user = await _userManager.Users
						.Include(u => u.Baskets)
						.FirstOrDefaultAsync(u => u.UserName == username);

				// Mevcut LINQ yapınla aktif sepeti bulalım
				var _basket =
					from basket in user.Baskets
					join order in _orderReadRepository.Table
					on basket.Id equals order.Id into BasketOrders
					from order in BasketOrders.DefaultIfEmpty()
					select new
					{
						Basket = basket,
						Order = order
					};

				Basket? targetBasket = null;

				// Siparişe dönüşmemiş (Order'ı null olan) bir sepet var mı?
				if (_basket.Any(b => b.Order is null))
				{
					targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
				}
				else
				{
					// Sepet yok, YENİ oluşturuluyor.
					targetBasket = new Basket();
					targetBasket.UserId = user.Id; // Kullanıcıyı ilişkilendir

					// HATA BURADAYDI: user.Baskets.Add yerine Repository kullanıyoruz.
					// Bu sayede EF bunun bir INSERT işlemi olduğunu kesin olarak anlar.
					await _basketWriteRepository.AddAsync(targetBasket);

					// Değişiklikleri kaydet
					await _basketWriteRepository.SaveAsync();
				}

				return targetBasket;
			}

			throw new Exception("Beklenmeyen hata: Kullanıcı bulunamadı.");
		}
		public async Task AddItemToBasketAsync(CreateBasketItemDto basketItem)
		{
			Basket? basket = await ContextUser();
			if (basket != null)
			{
				BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(
					   bi => bi.BasketId ==
					   basket.Id && bi.ProductId ==
					   Guid.Parse(basketItem.ProductId));

				if (_basketItem != null)
				{
					_basketItem.Quantity++;
				}
				else
				{
					await _basketItemWriteRepository.AddAsync(new()
					{
						BasketId = basket.Id,
						ProductId = Guid.Parse(basketItem.ProductId),
						Quantity = basketItem.Quantity
					});
					await _basketItemWriteRepository.SaveAsync();
				}

			}
		}

		public async Task<List<BasketItem>> GetBasketItemsAsync()
		{
			Basket? basket = await ContextUser();

			Basket? result = await
			_basketReadRepository.Table.Include(b => b.BasketItems)
				.ThenInclude(bi => bi.Product)
				.FirstOrDefaultAsync(b => b.Id == basket.Id);
			return result.BasketItems.ToList();
		}

		public async Task UpdateQuantityAsync(UpdateBasketItemDto basketItem)
		{
			BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
			if (_basketItem != null)
			{
				_basketItem.Quantity = basketItem.Quantity;
				await _basketItemWriteRepository.SaveAsync();
			}
		}

		public async Task DeleteItemAsync(string basketItemId)
		{
			BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
			if (basketItem != null)
			{
				_basketItemWriteRepository.Remove(basketItem);
				await _basketItemWriteRepository.SaveAsync();
			}
		}

		
	}
}
