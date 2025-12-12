using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.DealerProducts.Queries.GetDealerProductById
{
	public class GetDealerProductByIdQueryRequest:IRequest<GetDealerProductByIdQueryResponse>
	{
		public Guid ProductId { get; set; }
		public string UserId { get; set; } // Dealer UserId
	}
}
