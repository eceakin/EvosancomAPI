namespace EvosancomAPI.Application.Features.Dealers.Commands.CreateDealer
{
	public class CreateDealerCommandResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public Guid DealerId { get; set; }
		public string UserId { get; set; }
	}
}
