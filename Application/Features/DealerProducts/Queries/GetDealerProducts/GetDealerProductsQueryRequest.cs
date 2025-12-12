using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.DealerProducts.Queries.GetDealerProducts
{
	public class GetDealerProductsQueryRequest :IRequest<GetDealerProductsQueryResponse>
	{

		public string UserId { get; set; }

		public Guid? CategoryId { get; set; }

		public string? SearchTerm { get; set; }

		public decimal? MinPrice { get; set; }

		public decimal? MaxPrice { get; set; }

		public bool? IsCustomizable { get; set; }

		public string? OrderBy { get; set; }

		public int PageNumber { get; set; } = 1;

		public int PageSize { get; set; } = 10;
	}
	
}
