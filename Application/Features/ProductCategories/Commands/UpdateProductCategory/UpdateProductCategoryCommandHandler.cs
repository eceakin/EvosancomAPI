using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using MediatR;

namespace EvosancomAPI.Application.Features.ProductCategories.Commands.UpdateProductCategory
{
	public class UpdateProductCategoryCommandHandler :
		IRequestHandler<UpdateProductCategoryCommandRequest, UpdateProductCategoryCommandResponse>
	{
		readonly IProductCategoryWriteRepository _productCategoryWriteRepository;
		readonly IProductCategoryReadRepository _productCategoryReadRepository;
		public UpdateProductCategoryCommandHandler(IProductCategoryWriteRepository productCategoryWriteRepository, IProductCategoryReadRepository productCategoryReadRepository)
		{
			_productCategoryWriteRepository = productCategoryWriteRepository;
			_productCategoryReadRepository = productCategoryReadRepository;
		}
		public async Task<UpdateProductCategoryCommandResponse> Handle(UpdateProductCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			ProductCategory	productCategory =await _productCategoryReadRepository.GetByIdAsync(request.Id);
			productCategory.Name = request.Name;
			productCategory.UpdatedDate = DateTime.UtcNow;
			await _productCategoryWriteRepository.SaveAsync();
			return new();
		}
	}
}
