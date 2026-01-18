using EvosancomAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.DTOs.Order
{
	public class CreateOrderDto
	{
		public string BasketId { get; set; }
		public string ShippingAddress { get; set; }
		public DateTime? EstimatedDeliveryDate { get; set; }

	}
}
