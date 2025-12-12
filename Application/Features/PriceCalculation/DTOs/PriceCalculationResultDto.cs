using EvosancomAPI.Application.Features.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.PriceCalculation.DTOs
{
	public class PriceCalculationResultDto
	{
		public Guid ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal BasePrice { get; set; }
		public decimal CustomDimensionCost { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal DiscountAmount { get; set; }
		public decimal FinalPrice { get; set; }
		public bool HasCustomDimensions { get; set; }
		public CustomDimensionDto CustomDimensions { get; set; }
	}
}
