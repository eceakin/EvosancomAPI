using EvosancomAPI.Application.Features.Dealers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Queries.GetDealerDashboard
{
	public class GetDealerDashboardQueryResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public DealerDashboardDto Dashboard { get; set; }

	}
}
