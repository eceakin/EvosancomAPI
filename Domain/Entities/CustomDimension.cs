using EvosancomAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Domain.Entities
{
	public class CustomDimension : BaseEntity
	{
		public Guid OrderItemId { get; set; }
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Depth { get; set; }
		public decimal AdditionalCost { get; set; } // Özel ölçü ek maliyeti

		// Navigation Properties
		public OrderItem OrderItem { get; set; }
	}
}
