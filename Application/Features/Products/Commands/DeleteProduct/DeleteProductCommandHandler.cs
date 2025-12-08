using EvosancomAPI.Application.Commons.Interfaces;
using EvosancomAPI.Application.Commons.Models;
using EvosancomAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.Products.Commands.DeleteProduct
{


	public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
	{

		readonly IProductWriteRepository _productWriteRepository;

		public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
		{
			_productWriteRepository = productWriteRepository;
		}
		public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
		{
			await _productWriteRepository.RemoveAsync(request.Id);
			await _productWriteRepository.SaveAsync();
			return new DeleteProductCommandResponse()
			{
				Message = "Ürün başarıyla silindi."
			};


			
		}




	}
}