namespace EvosancomAPI.Application.Features.Products.DTOs
{
	public class ProductListDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Barcode { get; set; }
		public string Description { get; set; }
		public decimal BasePrice { get; set; }
		public bool IsCustomizable { get; set; }
		public string ImageUrl { get; set; }
		public bool IsActive { get; set; }
		public string CategoryName { get; set; }
	}
}
