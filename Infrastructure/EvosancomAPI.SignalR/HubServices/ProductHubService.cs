using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvosancomAPI.Application.Abstractions.Hubs;

namespace EvosancomAPI.SignalR.HubServices
{
	public class ProductHubService : IProductHubService
	{
		public Task ProductAddedMessageAsync(string message)
		{
			throw new NotImplementedException();
		}
	}
}
