using EvosancomAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Domain.Entities.Files
{
	public class File:BaseEntity
	{
		public string FileName { get; set; }
		public string Path { get; set; }
		public string Storage { get; set;}
		
	}
}
