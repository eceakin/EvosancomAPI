
using EvosancomAPI.Application.Commons.Interfaces;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Domain.Entities.Common;
using EvosancomAPI.Domain.Entities.Files;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Entities.Role;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DomainFile = EvosancomAPI.Domain.Entities.Files.File;


namespace EvosancomAPI.Persistence.Contexts
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>,IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<BasketItem> BasketItems { get; set; }
		public DbSet<Basket> Baskets { get; set; }
		public DbSet<Dealer> Dealers { get; set; }
	

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }


		public DbSet<DomainFile> Files { get; set; }

        public DbSet<Menu> Menus { get; set; }
		public DbSet<Endpoint> Endpoints { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Product>().Property(p => p.BasePrice).HasPrecision(18, 2);
			modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasPrecision(18, 2);
			modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
			modelBuilder.Entity<Dealer>()
				.HasOne(d => d.User)
				.WithOne() // ApplicationUser tarafında navigation property yok
				.HasForeignKey<Dealer>(d => d.UserId)
				;
			modelBuilder.Entity<Order>()
				.HasKey(d => d.Id);


			modelBuilder.Entity<Basket>()
				.HasOne(b => b.Order)
				.WithOne(o => o.Basket)
				.HasForeignKey<Order>(b => b.Id);
			base.OnModelCreating(modelBuilder);


			// Diğer ilişkiler EF Core'un "Convention" (Standart İsimlendirme) özelliği
			// sayesinde otomatik algılanır (CategoryId -> Category vb.) o yüzden hepsini yazmaya gerek yok.

			// İlişkiler, kısıtlamalar ve diğer yapılandırmalar burada yapılabilir
		}
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<BaseEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedDate = DateTime.UtcNow;
						entry.Entity.IsDeleted = false;
						break;
					case EntityState.Modified:
						entry.Entity.UpdatedDate = DateTime.UtcNow;
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

	}

}
