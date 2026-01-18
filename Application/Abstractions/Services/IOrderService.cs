using EvosancomAPI.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Abstractions.Services
{
	public interface IOrderService
	{
		Task<CreateOrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto);
	}
}
