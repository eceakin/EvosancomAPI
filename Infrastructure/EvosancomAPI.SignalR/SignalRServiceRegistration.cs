using EvosancomAPI.Application.Abstractions.Hubs;
using EvosancomAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.SignalR
{
	public static class SignalRServiceRegistration
	{
		public static IServiceCollection AddSignalRServices(this IServiceCollection services)
		{
			services.AddTransient<IProductHubService, ProductHubService>();
			services.AddSignalR();
			return services;
		}
	}
}
