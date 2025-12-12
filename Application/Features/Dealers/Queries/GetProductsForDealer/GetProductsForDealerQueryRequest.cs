using EvosancomAPI.Application.DTOs.Dealer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetProductsForDealer
{
	public class GetProductsForDealerQueryRequest: IRequest<List<DealerProductListDto>>
	{
		public string UserId { get; set; }
	}
}
