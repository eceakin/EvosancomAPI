namespace EvosancomAPI.Application.Features.Products.DTOs
{
	public class ProductCategoryDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public bool IsActive { get; set; }
		public string? ImageUrl { get; set; }
		public int DisplayOrder { get; set; }
		public int ProductCount { get; set; }
	}

}
