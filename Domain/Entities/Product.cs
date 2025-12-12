using EvosancomAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Domain.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public string Code { get; set; } // Ürün kodu
		public string Barcode { get; set; } // QR/Barkod için
		public Guid ProductCategoryId { get; set; }
		public int StockQuantity { get; set; }
		public string Description { get; set; }
		public decimal BasePrice { get; set; } // Temel fiyat
		public bool IsCustomizable { get; set; } // Özel ölçü yapılabilir mi?
		public string ImageUrl { get; set; }
		public bool IsActive { get; set; }

		// Navigation Properties
		public ProductCategory ProductCategory { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
		public ICollection<ProductionOrder> ProductionOrders { get; set; }
	}
}
