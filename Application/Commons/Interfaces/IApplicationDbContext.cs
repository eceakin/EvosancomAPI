using EvosancomAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainFile = EvosancomAPI.Domain.Entities.Files.File;

namespace EvosancomAPI.Application.Commons.Interfaces
{
	public interface IApplicationDbContext
	{
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


		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
