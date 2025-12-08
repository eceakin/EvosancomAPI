using EvosancomAPI.Application.Features.Dealers.DTOs;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerById
{
	public class GetDealerByIdQueryResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public DealerDto Dealer { get; set; }
	}

}
