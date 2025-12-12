using EvosancomAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.DTOs
{
	public class RecentOrderDto
	{
		public string Id { get; set; }
		public string OrderNumber { get; set; }
		public DateTime OrderDate { get; set; }

		public OrderStatus Status { get; set; }
		public string StatusText { get; set; }

		public decimal FinalAmount { get; set; }
	}

}
