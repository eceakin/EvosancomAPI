using EvosancomAPI.Application.Repositories.Endpoint;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.Endpoint
{
	public class EndpointWriteRepository :WriteRepository<Domain.Entities.Role.Endpoint> , IEndpointWriteRepository
	{
		public EndpointWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	
	}
}
