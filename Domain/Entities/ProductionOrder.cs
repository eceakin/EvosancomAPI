using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;

namespace EvosancomAPI.Domain.Entities
{
	// Domain/Entities/Production/ProductionOrder.cs
	public class ProductionOrder : BaseEntity
	{
		public Guid OrderId { get; set; }
		public Guid OrderItemId { get; set; }
		public Guid ProductId { get; set; }
		public string ProductionNumber { get; set; }
		public ProductionStatus Status { get; set; }
		public DateTime PlannedStartDate { get; set; }
		public DateTime? ActualStartDate { get; set; }
		public DateTime? EstimatedCompletionDate { get; set; }
		public DateTime? ActualCompletionDate { get; set; }
		public int CurrentStationId { get; set; }
		public string AssignedToUserId { get; set; }

		// Navigation Properties
		public Order Order { get; set; }
		public OrderItem OrderItem { get; set; }
		public Product Product { get; set; }
		public ApplicationUser AssignedToUser { get; set; }
		public ICollection<ProductionStation> ProductionStations { get; set; }
		public QualityControlForm QualityControlForm { get; set; }
	}

}
