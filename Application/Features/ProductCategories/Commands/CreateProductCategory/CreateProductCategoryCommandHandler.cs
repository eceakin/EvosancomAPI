using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using MediatR;

namespace EvosancomAPI.Application.Features.ProductCategories.Commands.CreateProductCategory
{
	public class CreateProductCategoryCommandHandler :
		IRequestHandler<CreateProductCategoryCommandRequest, CreateProductCategoryCommandResponse>
	{
		readonly IProductCategoryWriteRepository _productCategoryWriteRepository;

		public CreateProductCategoryCommandHandler(IProductCategoryWriteRepository productCategoryWriteRepository)
		{
			_productCategoryWriteRepository = productCategoryWriteRepository;
		}

		public async Task<CreateProductCategoryCommandResponse> Handle(CreateProductCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			var newProductCategory = new ProductCategory
			{
				Name = request.Name
			};

		await _productCategoryWriteRepository.AddAsync(newProductCategory);
		await _productCategoryWriteRepository.SaveAsync();

			return new CreateProductCategoryCommandResponse
			{
				Success = true,
				Message = "Product category başarıyla oluşturuldu.",
				CreatedProductCategoryId = newProductCategory.Id
			};
		}
	}
}
