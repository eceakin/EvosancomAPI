using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Persistence.Contexts;

namespace EvosancomAPI.Persistence.Repositories
{
	public class OrderItemWriteRepository : WriteRepository<OrderItem>, IOrderItemWriteRepository
	{
		public OrderItemWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
