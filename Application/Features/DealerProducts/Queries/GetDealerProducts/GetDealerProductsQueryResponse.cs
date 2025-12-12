using EvosancomAPI.Application.Features.DealerProducts.DTOs;
using EvosancomAPI.Domain.Entities.Common;

namespace EvosancomAPI.Application.Features.DealerProducts.Queries.GetDealerProducts
{
	public
		class GetDealerProductsQueryResponse
	{
		public bool Success { get; set; }

		public string? Message { get; set; }

		public List<DealerProductDto>? DealerProducts { get; set; }

		public decimal DealerDiscountRate { get; set; }

		public PaginationInfo PaginationInfo { get; set; }
	}
}
