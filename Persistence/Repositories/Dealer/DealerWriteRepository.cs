using EvosancomAPI.Application;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.Dealer
{
	public class DealerWriteRepository : WriteRepository<Domain.Entities.Dealer>, IDealerWriteRepository
	{
		public DealerWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
