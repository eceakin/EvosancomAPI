using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Application.Features.Orders.DTOs
{
	public class ProductionTrackingDto
	{
		public Guid ProductionOrderId { get; set; }

		public string ProductionNumber { get; set; }

		public ProductionStatus Status { get; set; }

		public int CurrentStationNumber { get; set; }

		public string CurrentStationName { get; set; }

		public int CompletedStations { get; set; }

		public int TotalStations { get; set; }

		public List<ProductionStationDto> ProductionStations { get; set; }

		public DateTime? EstimatedCompletionDate { get; set; }

		public DateTime? ActualCompletionDate { get; set; }
	}
}
