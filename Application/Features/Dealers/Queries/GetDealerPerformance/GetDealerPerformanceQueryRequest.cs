using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerPerformance
{
	public class GetDealerPerformanceQueryRequest:IRequest<GetDealerPerformanceQueryResponse>
	{
		public
			string UserId { get; set; }

	}

}
