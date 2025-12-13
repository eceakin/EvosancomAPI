using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Basket.Queries.GetBasketItems
{
	public class GetBasketItemsQueryRequest  : IRequest<List<GetBasketItemsQueryResponse>>
	{

	}
}
