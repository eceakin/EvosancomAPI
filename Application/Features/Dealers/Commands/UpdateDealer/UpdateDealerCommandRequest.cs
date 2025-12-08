using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Commands.UpdateDealer
{
	public class UpdateDealerCommandRequest:IRequest<UpdateDealerCommandResponse>
	{
		public Guid Id { get; set; }
		public string CompanyName { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public decimal DiscountRate { get; set; }
		public decimal MonthlySalesQuota { get; set; }
		public bool IsActive { get; set; }
		public DateTime? ContractEndDate { get; set; }
	}
}
