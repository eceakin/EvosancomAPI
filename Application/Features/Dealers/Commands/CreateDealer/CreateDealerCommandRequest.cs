using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Commands.CreateDealer
{
	public class CreateDealerCommandRequest:IRequest<CreateDealerCommandResponse>
	{
		// User bilgileri
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }

		// Dealer bilgileri
		public string CompanyName { get; set; }
		public string TaxNumber { get; set; }
		public string TaxOffice { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public decimal DiscountRate { get; set; } = 0;
		public decimal MonthlySalesQuota { get; set; } = 0;
		public DateTime ContractStartDate { get; set; }
		public DateTime? ContractEndDate { get; set; }

	}
}
