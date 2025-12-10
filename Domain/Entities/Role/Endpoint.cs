using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Domain.Entities.Role
{
	public class Endpoint:BaseEntity
	{
        public Endpoint() { 
        Roles = new HashSet<ApplicationRole>();
        }

        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Code { get; set; }
        public string Definition { get; set; }
        public Menu Menu { get; set; }
        public ICollection<ApplicationRole> Roles { get; set; }


    }
}
