namespace EvosancomAPI.Domain.Enums
{
	public enum ServiceRequestStatus
	{
		Submitted = 0,
		Assigned = 1,
		PickupScheduled = 2,
		InRepair = 3,
		RepairCompleted = 4,
		NotRepairable = 5,
		ReadyForDelivery = 6,
		Delivered = 7,
		Closed = 8
	}
}
