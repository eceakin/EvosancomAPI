using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Persistence.Contexts;

namespace EvosancomAPI.Persistence.Repositories
{
	public class ProductCategoryWriteRepository : WriteRepository<ProductCategory>, IProductCategoryWriteRepository
	{
		public ProductCategoryWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
