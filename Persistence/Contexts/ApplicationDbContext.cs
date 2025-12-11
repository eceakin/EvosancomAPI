
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

		public DbSet<Dealer> Dealers { get; set; }
		public DbSet<DealerSalesReport> SalesReports { get; set; }
		public DbSet<WarehouseEntry> WarehouseEntries { get; set; }
		public DbSet<ProductionOrder> ProductionOrders { get; set; }
		public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<ProductionStation> ProductionStations { get; set; }
		public DbSet<QualityControlForm> QualityControlForms { get; set; }

		public DbSet<Stock> Stocks { get; set; }
		public DbSet<StockMovement> StockMovements { get; set; }

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<Shipment> Shipments { get; set; }

		public DbSet<ServiceRequest> ServiceRequests { get; set; }

		public DbSet<Expense> Expenses { get; set; }
		public DbSet<DomainFile> Files { get; set; }

        public DbSet<Menu> Menus { get; set; }
		public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<DealerNotification> DealerNotifications { get; set; }
        public DbSet<PriceCalculationHistory>	 PriceCalculationHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Product>().Property(p => p.BasePrice).HasPrecision(18, 2);
			modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasPrecision(18, 2);
			modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
			modelBuilder.Entity<Expense>().Property(e => e.Amount).HasPrecision(18, 2);


		
			// Diğer ilişkiler EF Core'un "Convention" (Standart İsimlendirme) özelliği
			// sayesinde otomatik algılanır (CategoryId -> Category vb.) o yüzden hepsini yazmaya gerek yok.

			base.OnModelCreating(modelBuilder);
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
