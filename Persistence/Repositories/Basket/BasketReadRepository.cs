using EvosancomAPI.Application.Repositories.Basket;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.Basket
{
	public class BasketReadRepository : ReadRepository<Domain.Entities.Basket>, IBasketReadRepository
	{
		public BasketReadRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
