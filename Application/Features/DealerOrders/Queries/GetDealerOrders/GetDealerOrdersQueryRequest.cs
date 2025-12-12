using EvosancomAPI.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.DealerOrders.Queries.GetDealerOrders
{
	public class GetDealerOrdersQueryRequest :IRequest<GetDealerOrdersQueryResponse>
	{
        public string UserId { get; set; }
		public OrderStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string? SearchTerm { get; set; }
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public string? OrderBy { get; set; } = "date_desc";

    }
	
}
