using EvosancomAPI.Application;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.Endpoint
{
	public class EndpointReadRepository:ReadRepository<Domain.Entities.Role.Endpoint>, IEndpointReadRepository
	{
		public EndpointReadRepository(ApplicationDbContext context) : base(context)
		{
		}
	
	}
}
