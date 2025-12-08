namespace EvosancomAPI.Domain.Enums
{
	public enum ProductionStatus
	{
		Planned = 0,
		InProgress = 1,
		Station1_MetalCutting = 2,
		Station2_Painting = 3,
		Station3_Insulation = 4,
		Station4_Assembly = 5,
		Station5_FinalTests = 6,
		QualityControlPending = 7,
		Completed = 8,
		Failed = 9
	}
}
