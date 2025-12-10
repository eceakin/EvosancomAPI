using EvosancomAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Domain.Entities.Role
{
	public class Menu :BaseEntity
	{
        public string Name { get; set; }
		public ICollection<Endpoint> Endpoints { get; set; }
    }
}
