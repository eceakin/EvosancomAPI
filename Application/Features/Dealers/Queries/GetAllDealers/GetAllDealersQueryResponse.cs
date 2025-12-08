using EvosancomAPI.Domain.Entities;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetAllDealers
{
	public class GetAllDealersQueryResponse
	{
		public List<DealerListDto> Dealers { get; set; }
		public int TotalDealerCount { get; set; }

	}
}
