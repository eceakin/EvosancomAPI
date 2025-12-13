using EvosancomAPI.Application.Abstractions.Hubs;
using Microsoft.Extensions.DependencyInjection;
using SignalR.HubServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddSignalRServices(this IServiceCollection services)
		{
			services.AddTransient<IProductHubService , ProductHubService>();
			services.AddTransient<IOrderHubService , OrderHubService>();
			services.AddSignalR();
			return services;
		}
	}
}
