using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Persistence.Contexts;

namespace EvosancomAPI.Persistence.Repositories
{
	public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
	{
		public OrderWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
