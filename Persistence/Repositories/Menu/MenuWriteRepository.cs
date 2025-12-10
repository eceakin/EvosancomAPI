using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.Menu
{
	public class MenuWriteRepository : WriteRepository<Domain.Entities.Role.Menu>, IMenuWriteRepository
	{
		public MenuWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
