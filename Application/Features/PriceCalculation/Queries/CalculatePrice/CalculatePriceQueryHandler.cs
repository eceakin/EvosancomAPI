using MediatR;

namespace EvosancomAPI.Application.Features.PriceCalculation.Queries.CalculatePrice
{
	public class CalculatePriceQueryHandler : IRequestHandler<CalculatePriceQueryRequest, CalculatePriceQueryResponse>
	{
		public Task<CalculatePriceQueryResponse> Handle(CalculatePriceQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
