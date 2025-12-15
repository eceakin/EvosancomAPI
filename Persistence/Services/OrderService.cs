using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.Order;
using EvosancomAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Services
{
	public class OrderService : IOrderService
	{
		readonly IOrderWriteRepository _orderWriteRepository;

		public OrderService(IOrderWriteRepository orderWriteRepository)
		{
			_orderWriteRepository = orderWriteRepository;
		}

		public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
		{
			await _orderWriteRepository.AddAsync(new()
			{
				ShippingAddress = createOrderDto.ShippingAddress,
				Id = Guid.Parse(createOrderDto.BasketId),
				OrderDate = createOrderDto.OrderDate , 
				Status = createOrderDto.Status , 
				TotalAmount = createOrderDto.TotalAmount , 
				DiscountAmount = createOrderDto.DiscountAmount , 
				FinalAmount = createOrderDto.FinalAmount

			});
			await _orderWriteRepository.SaveAsync();
		}
	}
}
