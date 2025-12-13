using EvosancomAPI.Application.Repositories.BasketItems;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.BasketItem
{
	public class BasketItemReadRepository :ReadRepository<Domain.Entities.BasketItem>, IBasketItemReadRepository
	{
		public BasketItemReadRepository(ApplicationDbContext context) : base(context)
		{
		}
	
	}

}
