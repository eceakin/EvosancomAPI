namespace EvosancomAPI.Application.Features.ProductCategories.Commands.CreateProductCategory
{
	public class CreateProductCategoryCommandResponse
	{
        public bool Success { get; set; }
		public string Message { get; set; }
		public Guid CreatedProductCategoryId { get; set; }
    }
}
