using Microsoft.EntityFrameworkCore;
using Store.Database.Domain.Products;
using Store.Database.Domain.Categories;
using Store.Database.Domain.Parameters;
using Store.Database.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Store.Database.Domain.Customers;
using Store.Database.Domain.Orders;
using Store.Database.Domain.Payments;
using Store.Database.Domain.Catalogs;
using Store.Database.Domain.Coupons;
using Store.Database.Domain.Payments;

namespace Store.Database
{
    public class StoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DocType> DocTypes { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Products", "Catalog");
            modelBuilder.Entity<Category>().ToTable("Categories", "Catalog");

            modelBuilder.Entity<Customer>().ToTable("Customers", "Customer");
            modelBuilder.Entity<DocType>().ToTable("DocTypes", "Parameter");
            modelBuilder.Entity<Country>().ToTable("Countries", "Parameter");

            modelBuilder.Entity<Order>().ToTable("Orders", "Order");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails", "Order");

            modelBuilder.Entity<Payment>().ToTable("Payments", "Payment");
            modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethods", "Payment");
            modelBuilder.Entity<Parameter>().ToTable("Parameters", "Parameter");

            modelBuilder.Entity<Coupon>().ToTable("Coupons", "Coupon");

            modelBuilder.Entity<Inventory>().ToTable("Inventories", "Inventory");

            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.DocTypeId, c.DocNumber })
                .IsUnique()
                .HasDatabaseName("IX_DocID");

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Customer)
                .WithOne()
                .HasForeignKey<ApplicationUser>(u => u.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany() 
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithOne()
                .HasForeignKey<Inventory>(i => i.ProductId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentMethod)
                .WithMany()
                .HasForeignKey(p => p.PaymentMethodId);
        }
    }
}
