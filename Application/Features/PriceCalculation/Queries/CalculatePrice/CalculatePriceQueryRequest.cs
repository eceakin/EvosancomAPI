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
	}
}
