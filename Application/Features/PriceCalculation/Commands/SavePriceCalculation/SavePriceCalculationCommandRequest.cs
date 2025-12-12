using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.PriceCalculation.Commands.SavePriceCalculation
{
	public class SavePriceCalculationCommandRequest :IRequest<SavePriceCalculationCommandResponse>
	{
		public string UserId { get; set; }
		public Guid ProductId { get; set; }

		// Custom Dimensions (nullable)
		public decimal? Width { get; set; }
		public decimal? Height { get; set; }
		public decimal? Depth { get; set; }

		// Calculation Values
		public decimal BasePrice { get; set; }
		public decimal CustomDimensionCost { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal DiscountedPrice { get; set; }

	}
}
