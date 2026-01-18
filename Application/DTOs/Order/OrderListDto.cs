using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.DTOs.Order
{
	public class OrderListDto
	{
		public Guid Id { get; set; }
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }
		public decimal TotalAmount { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalAmount { get; set; }
		public string ShippingAddress { get; set; }
		public DateTime? EstimatedDeliveryDate { get; set; }
		public DateTime? ActualDeliveryDate { get; set; }
		public int ItemCount { get; set; }
	}
}
