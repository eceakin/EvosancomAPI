using EvosancomAPI.Application.Features.Orders.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.PriceCalculation.Queries.CalculatePrice
{
	public class CalculatePriceQueryRequest:IRequest<CalculatePriceQueryResponse>
	{
		public Guid ProductId { get; set; }
		public string UserId { get; set; }  // Dealer UserId

		// Özel ölçüler: genişlik, yükseklik vb.
		public CustomDimensionDto? CustomDimension { get; set; }
	}
}
