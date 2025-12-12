using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Domain.Entities.Common
{

		public class PaginationInfo
		{
			public int CurrentPage { get; set; }

			public int PageSize { get; set; }

			public int TotalCount { get; set; }

			public int TotalPages { get; set; }
		}
	

}
