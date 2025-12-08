using EvosancomAPI.Domain.Entities.Common;

namespace EvosancomAPI.Domain.Entities
{
	public class QualityControlForm : BaseEntity
	{
		public Guid ProductionOrderId { get; set; }
		public string Station1Notes { get; set; } // Metal Kesimi
		public bool Station1Approved { get; set; }
		public string Station2Notes { get; set; } // Boyama
		public bool Station2Approved { get; set; }
		public string Station3Notes { get; set; } // İzolasyon
		public bool Station3Approved { get; set; }
		public string Station4Notes { get; set; } // Montaj
		public bool Station4Approved { get; set; }
		public string Station5Notes { get; set; } // Son Testler
		public bool Station5Approved { get; set; }
		public bool FinalApproval { get; set; }
		public DateTime? CompletedDate { get; set; }

		// Navigation Properties
		public ProductionOrder ProductionOrder { get; set; }
	}

}
