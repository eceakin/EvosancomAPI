using EvosancomAPI.Application.Features.ProductCategories.DTOs;

namespace EvosancomAPI.Application.Features.ProductCategories.Queries.GetAllProductCategories
{
	public class GetAllProductCategoriesQueryResponse
	{
        public List<ProductCategoryDto> ProductCategories { get; set; }
        public int TotalProductCategoryCount { get; set; }
    }
}
