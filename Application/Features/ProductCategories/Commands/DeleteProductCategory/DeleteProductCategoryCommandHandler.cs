using EvosancomAPI.Application.Repositories;
using MediatR;

namespace EvosancomAPI.Application.Features.ProductCategories.Commands.DeleteProductCategory
{
	public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommandRequest, DeleteProductCategoryCommandResponse>
	{
		readonly IProductCategoryWriteRepository _productCategoryWriteRepository;

		public DeleteProductCategoryCommandHandler(IProductCategoryWriteRepository productCategoryWriteRepository)
		{
			_productCategoryWriteRepository = productCategoryWriteRepository;
		}

		public async Task<DeleteProductCategoryCommandResponse> Handle(DeleteProductCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			await _productCategoryWriteRepository.RemoveAsync(request.Id);
			await _productCategoryWriteRepository.SaveAsync();
			return new DeleteProductCategoryCommandResponse()
			{
				Message = "Ürün kategorisi başarıyla silindi."
			};
		}
	}
}
