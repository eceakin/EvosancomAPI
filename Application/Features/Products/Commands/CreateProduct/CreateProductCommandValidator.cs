using FluentValidation;

namespace EvosancomAPI.Application.Features.Products.Commands.CreateProduct
{
	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Ürün adı gereklidir.")
				.MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir.");

			//RuleFor(x => x.Code)
			//	.NotEmpty().WithMessage("Ürün kodu gereklidir.")
			//	.MaximumLength(50).WithMessage("Ürün kodu en fazla 50 karakter olabilir.");

			RuleFor(x => x.ProductCategoryId)
				.NotEmpty().WithMessage("Kategori seçimi gereklidir.");

			RuleFor(x => x.BasePrice)
				.GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");

			RuleFor(x => x.Description)
				.MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");
		}
	}
}