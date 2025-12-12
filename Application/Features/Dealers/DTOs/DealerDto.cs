using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.DTOs
{
	public class DealerDto
	{

		public Guid Id { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string CompanyName { get; set; }
		public string TaxNumber { get; set; }
		public string TaxOffice { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal MonthlySalesQuota { get; set; }
		public bool IsActive { get; set; }
		public DateTime ContractStartDate { get; set; }
		public DateTime? ContractEndDate { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
