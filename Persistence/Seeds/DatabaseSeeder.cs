using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Seeds
{
	public static class DatabaseSeeder
	{
		public static async Task SeedAsync(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
			var logger = serviceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

			try
			{
				// Veritabanını güncelle/oluştur
				await context.Database.MigrateAsync();

				// Rolleri oluştur
				await SeedRolesAsync(roleManager, logger);

				// Admin kullanıcısı oluştur
				await SeedAdminUserAsync(userManager, roleManager, logger);

				logger.LogInformation("Database seeding completed successfully.");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while seeding the database.");
				throw;
			}
		}

		private static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager, ILogger logger)
		{
			string[] roles = {
				"Admin",
				"FabrikaYonetimi",
				"Bayi",
				"Musteri",
				"UretimSahasi",
				"Depo",
				"Muhasebe",
				"TeknikServis"
			};

			foreach (var roleName in roles)
			{
				if (!await roleManager.RoleExistsAsync(roleName))
				{
					var role = new ApplicationRole
					{
						Id = Guid.NewGuid().ToString(),
						Name = roleName,
						NormalizedName = roleName.ToUpper()
					};

					var result = await roleManager.CreateAsync(role);

					if (result.Succeeded)
					{
						logger.LogInformation($"Role '{roleName}' created successfully.");
					}
					else
					{
						logger.LogError($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
					}
				}
			}
		}

		private static async Task SeedAdminUserAsync(
			UserManager<ApplicationUser> userManager,
			RoleManager<ApplicationRole> roleManager,
			ILogger logger)
		{
			const string adminEmail = "admin@com";
			const string adminUsername = "admin";
			const string adminPassword = "admin"; // ÜRETİMDE MUTLAKA DEĞİŞTİRİN!

			// Admin kullanıcısı var mı kontrol et
			var adminUser = await userManager.FindByEmailAsync(adminEmail);

			if (adminUser == null)
			{
				// Admin kullanıcısı oluştur
				adminUser = new ApplicationUser
				{
					Id = Guid.NewGuid().ToString(),
					UserName = adminUsername,
					Email = adminEmail,
					EmailConfirmed = true,
					FirstName = "System",
					LastName = "Administrator",
					DateOfBirth = new DateTime(1990, 1, 1),
					CreatedDate = DateTime.UtcNow
				};

				var createResult = await userManager.CreateAsync(adminUser, adminPassword);

				if (createResult.Succeeded)
				{
					logger.LogInformation("Admin user created successfully.");

					// Admin rolünü ata
					var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");

					if (roleResult.Succeeded)
					{
						logger.LogInformation("Admin role assigned to admin user.");
					}
					else
					{
						logger.LogError($"Failed to assign admin role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
					}
				}
				else
				{
					logger.LogError($"Failed to create admin user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
				}
			}
			else
			{
				logger.LogInformation("Admin user already exists.");

				// Admin rolü atanmış mı kontrol et
				if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
				{
					var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
					if (roleResult.Succeeded)
					{
						logger.LogInformation("Admin role assigned to existing admin user.");
					}
				}
			}
		}
	}
}
