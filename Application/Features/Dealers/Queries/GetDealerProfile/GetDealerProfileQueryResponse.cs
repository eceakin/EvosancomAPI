using EvosancomAPI.Application.Features.Dealers.DTOs;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerProfile
{
	public class GetDealerProfileQueryResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public DealerDto Profile { get; set; }
	}
}
