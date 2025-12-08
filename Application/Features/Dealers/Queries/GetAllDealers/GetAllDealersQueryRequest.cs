using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetAllDealers
{
	public class GetAllDealersQueryRequest:IRequest<GetAllDealersQueryResponse>
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public bool? IsActive { get; set; }
		public string? SearchTerm { get; set; }

	}
}
