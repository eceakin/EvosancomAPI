using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.Dealer
{
	public class DealerReadRepository : ReadRepository<Domain.Entities.Dealer>, IDealerReadRepository
	{
		public DealerReadRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
