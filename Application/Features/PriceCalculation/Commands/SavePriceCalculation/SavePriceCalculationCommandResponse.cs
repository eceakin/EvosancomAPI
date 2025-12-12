namespace EvosancomAPI.Application.Features.PriceCalculation.Commands.SavePriceCalculation
{
	public class SavePriceCalculationCommandResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }

		// Kayıt edilen hesaplamanın Id'si
		public Guid CalculationId { get; set; }
	}
}
