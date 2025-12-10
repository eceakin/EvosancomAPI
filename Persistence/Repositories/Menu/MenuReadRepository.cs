using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.Menu
{
	public class MenuReadRepository:ReadRepository<Domain.Entities.Role.Menu> , IMenuReadRepository
	{
		public MenuReadRepository(ApplicationDbContext context) : base(context)
		{
		}
	
	}
}
