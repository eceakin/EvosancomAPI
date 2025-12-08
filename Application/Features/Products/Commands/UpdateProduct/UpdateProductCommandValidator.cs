using FluentValidation;

namespace EvosancomAPI.Application.Features.Products.Commands.UpdateProduct
{

	
	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage("Ürün ID gereklidir.");

			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Ürün adı gereklidir.")
				.MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir.");

			RuleFor(x => x.Code)
				.NotEmpty().WithMessage("Ürün kodu gereklidir.")
				.MaximumLength(50).WithMessage("Ürün kodu en fazla 50 karakter olabilir.");

			RuleFor(x => x.ProductCategoryId)
				.NotEmpty().WithMessage("Kategori seçimi gereklidir.");

			RuleFor(x => x.BasePrice)
				.GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");
		}
	}
}
