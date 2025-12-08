using EvosancomAPI.Application.Abstractions.Storage;
using EvosancomAPI.Application.Abstractions.Token;
using EvosancomAPI.Infrastructure.enums;
using EvosancomAPI.Infrastructure.Services.Storage;
using EvosancomAPI.Infrastructure.Services.Storage.Local;
using EvosancomAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Infrastructure
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddScoped<IStorageService, StorageService>();
			services.AddScoped<IStorage, LocalStorage>();
			services.AddScoped<ITokenHandler, TokenHandler>();

		}
		public static void AddStorage<T>(this IServiceCollection services,StorageType storageType) where T : class, IStorage
		{
			switch (storageType)
			{
				case StorageType.Local:
					services.AddScoped<IStorage, LocalStorage>();
					break;
				case StorageType.Azure:
					break;
				case StorageType.AWS:
					break;
				default:
					services.AddScoped<IStorage, LocalStorage>();
					break;
			}
		}

	}
}
