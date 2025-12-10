using EvosancomAPI.Domain.Entities.Role;
using Microsoft.AspNetCore.Identity;

namespace EvosancomAPI.Domain.Entities.Identity
{
	public class ApplicationRole : IdentityRole <string>
	{
        public ICollection<Endpoint> Endpoints { get; set; }

    }
}
