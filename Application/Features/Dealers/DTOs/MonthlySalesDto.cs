using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.DTOs
{
	public class MonthlySalesDto
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public string MonthName { get; set; }
		public decimal TotalSales { get; set; }
	}

}
