using EvosancomAPI.Domain.Entities.Common;

namespace EvosancomAPI.Domain.Entities
{
	public class ProductCategory : BaseEntity
	{
		public string Name { get; set; } // Örn: Meşrubat Dolabı, Dondurma Dolabı [cite: 53]
		public virtual ICollection<Product> Products { get; set; }
	}
}