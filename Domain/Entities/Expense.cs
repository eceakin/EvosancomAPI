using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Domain.Entities
{
	public class Expense : BaseEntity
	{
		public string ExpenseNumber { get; set; }
		public ExpenseType Type { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public DateTime ExpenseDate { get; set; }
		public string Category { get; set; }
		public string RecordedByUserId { get; set; }
		public string ApprovedByUserId { get; set; }
		public bool IsApproved { get; set; }
		public DateTime? ApprovalDate { get; set; }
		public string InvoiceNumber { get; set; }
		public string Vendor { get; set; }

		// Navigation Properties
		public ApplicationUser RecordedByUser { get; set; }
		public ApplicationUser ApprovedByUser { get; set; }
	}

}
