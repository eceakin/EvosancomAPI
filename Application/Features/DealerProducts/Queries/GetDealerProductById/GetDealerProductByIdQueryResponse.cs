using EvosancomAPI.Application.Features.DealerProducts.DTOs;

namespace EvosancomAPI.Application.Features.DealerProducts.Queries.GetDealerProductById
{
	public class GetDealerProductByIdQueryResponse
	{
		public bool Success { get; set; }
		public DealerProductDetailDto Product { get; set; }
		public string Message { get; set; }
	}
}
