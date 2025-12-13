using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.DTOs.BasketItem
{
	public class CreateBasketItemDto
	{
		public string ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
