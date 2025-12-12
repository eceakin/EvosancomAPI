using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.DTOs.Dealer
{
	public class DealerProductListDto
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int StockQuantity { get; set; }
		public decimal BasePrice { get; set; } // Normal Fiyat
		public decimal DiscountedPrice { get; set; } // Bayiye Özel Fiyat
		public decimal AppliedDiscountRate { get; set; } // Uygulanan İskonto Oranı
		public int Stock { get; set; }
	}
}
