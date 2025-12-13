using EvosancomAPI.Application.Repositories.BasketItems;
using EvosancomAPI.Persistence.Contexts;

namespace EvosancomAPI.Persistence.Repositories.BasketItem
{
	public class BasketItemWriteRepository : WriteRepository<Domain.Entities.BasketItem>, IBasketItemWriteRepository
	{
		public BasketItemWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}

}
