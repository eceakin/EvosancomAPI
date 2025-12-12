using EvosancomAPI.Application.Features.Orders.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.DealerOrders.Commands.CreateDealerOrder
{
	public class CreateDealerOrderCommandRequest : IRequest<CreateDealerOrderCommandResponse>
	{
		public string UserId { get; set; }
		public List<OrderItemDto> OrderItems { get; set; }
		public string ShippingAddress { get; set; }
		public string? BillingAddress { get; set; }
		public string? Notes { get; set; }
	}
}
