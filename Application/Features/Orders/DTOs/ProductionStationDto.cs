using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Application.Features.Orders.DTOs
{
	public class ProductionStationDto
	{
		public int StationNumber { get; set; }

		public string StationName { get; set; }

		public StationStatus Status { get; set; }

		public string StatusText { get; set; }

		public DateTime? StartTime { get; set; }

		public DateTime? EndTime { get; set; }
	}
}
