using MediatR;

namespace EvosancomAPI.Application.Features.Dealers.Commands.UpdateDealer
{
	public class UpdateDealerCommandRequest : IRequest<UpdateDealerCommandResponse>
	{
		public string Id { get; set; } // Dealer Id
		public decimal DiscountRate { get; set; }
		public decimal SalesQuota { get; set; }
		public string CompanyName { get; set; }
	}
}