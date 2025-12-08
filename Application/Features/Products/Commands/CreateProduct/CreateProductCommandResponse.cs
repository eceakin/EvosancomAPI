using EvosancomAPI.Application.Features.Products.DTOs;

namespace EvosancomAPI.Application.Features.Products.Commands.CreateProduct
{
	public class CreateProductCommandResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public Guid CreatedProductId { get; set; }
	}
}
