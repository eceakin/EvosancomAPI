using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerProfile
{
	public class GetDealerProfileQueryRequest:IRequest<GetDealerProfileQueryResponse>
	{
		public string UserId { get; set; }
	}
}
