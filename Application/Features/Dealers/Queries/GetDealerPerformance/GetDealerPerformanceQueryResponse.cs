using EvosancomAPI.Application.Features.Dealers.DTOs;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerPerformance
{
	public class GetDealerPerformanceQueryResponse
	{
        public bool Success { get; set; }
        public string Message { get; set; }
        public DealerPerformanceDto      Performance { get; set; }
    }

}
