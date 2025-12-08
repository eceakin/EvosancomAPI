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
	public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
	{
		public OrderReadRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
