using MediatR;

namespace EvosancomAPI.Application.Features.PriceCalculation.Commands.SavePriceCalculation
{
	public class SavePriceCalculationCommandHandler : IRequestHandler<SavePriceCalculationCommandRequest, SavePriceCalculationCommandResponse>
	{
		public Task<SavePriceCalculationCommandResponse> Handle(SavePriceCalculationCommandRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
