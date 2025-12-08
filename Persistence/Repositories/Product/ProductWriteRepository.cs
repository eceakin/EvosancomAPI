using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Persistence.Contexts;

namespace EvosancomAPI.Persistence.Repositories
{
	public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
	{
		public ProductWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
