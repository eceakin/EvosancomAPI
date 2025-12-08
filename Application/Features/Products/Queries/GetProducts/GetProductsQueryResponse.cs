using EvosancomAPI.Application.Features.Products.DTOs;
using System.Collections.Generic;

namespace EvosancomAPI.Application.Features.Products.Queries.GetProducts
{
	public class GetProductsQueryResponse
	{
		public List<ProductListDto> Products { get; set; }
		public int TotalProductCount { get; set; }
	}
}
