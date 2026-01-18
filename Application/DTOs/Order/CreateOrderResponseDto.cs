using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.DTOs.Order
{
	public class CreateOrderResponseDto
	{
		public Guid OrderId { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalAmount { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime? EstimatedDeliveryDate { get; set; }

	}
}
