using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application
{
	public interface IEndpointReadRepository:IReadRepository<Endpoint>
	{
	}
}
