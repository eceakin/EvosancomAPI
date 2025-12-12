using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.DTOs.Dealer
{
	public class CreateDealerDto
	{
		public string UserId { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal SalesQuota { get; set; }
		public string CompanyName { get; set; }


	}
}