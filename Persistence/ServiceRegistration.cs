using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EvosancomAPI.Persistence.Contexts;
using EvosancomAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI;
using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Persistence.Services;
using EvosancomAPI.Application.Abstractions.Services.Authentications;
using Microsoft.AspNetCore.Identity;
using EvosancomAPI.Application.Repositories.Dealer;
using EvosancomAPI.Application;
using EvosancomAPI.Persistence.Repositories.Dealer;
using EvosancomAPI.Persistence.Repositories.Endpoint;
using EvosancomAPI.Application.Repositories.Endpoint;
using EvosancomAPI.Persistence.Repositories.Menu;
using EvosancomAPI.Application.Repositories.PriceCalculation;

using EvosancomAPI.Persistence.Repositories.DealerNotification;
using EvosancomAPI.Application.Repositories.DealerNotification;
using EvosancomAPI.Persistence.Repositories.PriceCalculation;
using EvosancomAPI.Application.Repositories.BasketItems;
using EvosancomAPI.Persistence.Repositories.BasketItem;
using EvosancomAPI.Application.Repositories.Basket;
using EvosancomAPI.Persistence.Repositories.Basket;



namespace EvosancomAPI.Persistence
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
			);

			// Identity ayarları - SignInManager ile birlikte
			services.AddIdentityCore<ApplicationUser>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 3;
			})
			.AddRoles<ApplicationRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddSignInManager<SignInManager<ApplicationUser>>() // BU SATIRI EKLEDİK
			.AddDefaultTokenProviders();

			services.AddScoped<IOrderReadRepository, OrderReadRepository>();
			services.AddScoped<IProductCategoryReadRepository, ProductCategoryReadRepository>();
			services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
			services.AddScoped<IProductReadRepository, ProductReadRepository>();
			services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
			services.AddScoped<IProductCategoryWriteRepository, ProductCategoryWriteRepository>();
			services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();
			services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
			services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
			services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();

			services.AddScoped<IMenuReadRepository, MenuReadRepository>();
			services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IInternalAuthentication, AuthService>();
			services.AddScoped<IExternalAuthentication, AuthService>();
			services.AddScoped<IRoleService , RoleService>();
			services.AddScoped<IDealerService , DealerService>();

			services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();

			services.AddScoped<IDealerReadRepository, DealerReadRepository>();
			services.AddScoped<IDealerWriteRepository, DealerWriteRepository>();

			services.AddScoped<IDealerNotificationReadRepository, DealerNotificationReadRepository>();
			services.AddScoped<IDealerNotificationWriteRepository, DealerNotificationWriteRepository>();

			services.AddScoped<IPriceCalculationHistoryReadRepository, PriceCalculationHistoryReadRepository>();
			services.AddScoped<IPriceCalculationHistoryWriteRepository, PriceCalculationHistoryWriteRepository>();

			services.AddScoped<IAuthorizationEndpointService , AuthorizationEndpointService>();


			services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
			services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();

			services.AddScoped<IBasketReadRepository, BasketReadRepository>();
			services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

			services.AddScoped<IBasketService, BasketService>();
			services.AddScoped<IOrderService, OrderService>();


			return services;
		}
	}
}