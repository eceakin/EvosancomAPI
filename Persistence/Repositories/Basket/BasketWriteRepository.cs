using EvosancomAPI.Application.Repositories.Basket;
using EvosancomAPI.Persistence.Contexts;

namespace EvosancomAPI.Persistence.Repositories.Basket
{
	public class BasketWriteRepository : WriteRepository<Domain.Entities.Basket>, IBasketWriteRepository
	{
		public BasketWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
