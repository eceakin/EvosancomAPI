using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories
{
	public class OrderItemReadRepository : ReadRepository<OrderItem>, IOrderItemReadRepository
	{
		public OrderItemReadRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
