using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerById
{
	public class GetDealerByIdQueryRequest : IRequest<GetDealerByIdQueryResponse>
	{
		public string Id { get; set; }
	}
}
