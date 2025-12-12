using EvosancomAPI.Application.Features.PriceCalculation.DTOs;

namespace EvosancomAPI.Application.Features.PriceCalculation.Queries.CalculatePrice
{
	public class CalculatePriceQueryResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }

		// Hesaplama sonucu DTO
		public PriceCalculationResultDto? Calculation { get; set; }
	}
}
