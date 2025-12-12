using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.DealerProducts.DTOs
{
	public class DealerProductDto
	{
		public Guid ProductId { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		public string Barcode { get; set; }

		public Guid CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string Description { get; set; }

		public decimal BasePrice { get; set; }

		public decimal DiscountRate { get; set; }

		public decimal DiscountAmount { get; set; }

		public decimal DiscountedPrice { get; set; }

		public bool IsCustomizable { get; set; }

		public string? ImageUrl { get; set; }
	}
}
