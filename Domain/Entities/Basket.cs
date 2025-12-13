using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Domain.Entities
{
	public class Basket :BaseEntity
	{
		public string UserId { get; set; }

		public ApplicationUser User { get; set; }
		public Order Order { get; set; }

		public ICollection<BasketItem> BasketItems { get; set; }
	}
}
