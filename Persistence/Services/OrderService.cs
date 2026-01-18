using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.Order;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Application.Repositories.Basket;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Persistence.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderWriteRepository _orderWriteRepository;
		private readonly IBasketReadRepository _basketReadRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IDealerReadRepository _dealerReadRepository;

		public OrderService(
			IOrderWriteRepository orderWriteRepository,
			IBasketReadRepository basketReadRepository,
			UserManager<ApplicationUser> userManager,
			IDealerReadRepository dealerReadRepository)
		{
			_orderWriteRepository = orderWriteRepository;
			_basketReadRepository = basketReadRepository;
			_userManager = userManager;
			_dealerReadRepository = dealerReadRepository;
		}

		public async Task<CreateOrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto)
		{
			// Get basket with items and products
			var basket = await _basketReadRepository.Table
				.Include(b => b.BasketItems)
				.ThenInclude(bi => bi.Product)
				.FirstOrDefaultAsync(b => b.Id == Guid.Parse(createOrderDto.BasketId));

			if (basket == null)
				throw new Exception("Sepet bulunamadı.");

			if (!basket.BasketItems.Any())
				throw new Exception("Sepet boş. Sipariş oluşturulamaz.");

			// Calculate totals
			decimal totalAmount = 0;
			decimal discountAmount = 0;
			// Check if user is a dealer
			var user = await _userManager.FindByIdAsync(basket.UserId);
			var dealer = await _dealerReadRepository.GetSingleAsync(d => d.UserId == user.Id);

			if (dealer != null)
			{
				discountAmount = totalAmount * dealer.DiscountRate;
			}

			foreach (var item in basket.BasketItems)
			{
				var itemTotal = item.Product.BasePrice * item.Quantity;
				totalAmount += itemTotal;
			}

			// You can add discount logic here based on:
			// - User type (Dealer vs regular customer)
			// - Promotional codes
			// - Quantity discounts
			// For now, we'll set discount to 0
			discountAmount = 0;

			decimal finalAmount = totalAmount - discountAmount;

			// Calculate estimated delivery date (e.g., 10 business days from now)
			DateTime estimatedDeliveryDate = createOrderDto.EstimatedDeliveryDate
				?? DateTime.UtcNow.AddDays(10);

			// Create order
			var order = new Order
			{
				Id = Guid.Parse(createOrderDto.BasketId), // Same ID as basket
				OrderDate = DateTime.UtcNow,
				Status = OrderStatus.Pending,
				TotalAmount = totalAmount,
				DiscountAmount = discountAmount,
				FinalAmount = finalAmount,
				ShippingAddress = createOrderDto.ShippingAddress,
				EstimatedDeliveryDate = estimatedDeliveryDate
			};

			await _orderWriteRepository.AddAsync(order);
			await _orderWriteRepository.SaveAsync();

			return new CreateOrderResponseDto
			{
				OrderId = order.Id,
				TotalAmount = totalAmount,
				DiscountAmount = discountAmount,
				FinalAmount = finalAmount,
				OrderDate = order.OrderDate,
				EstimatedDeliveryDate = estimatedDeliveryDate
			};
		}
	}
}