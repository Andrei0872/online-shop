using System.Reflection.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using server.Models;

namespace server.Data
{

    public class ServerContext : IdentityDbContext<
            User, Role, int,
            IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
            IdentityRoleClaim<int>, IdentityUserToken<int>
        >
    {
    public ServerContext(DbContextOptions options) : base(options) {}

    // Defining the entities.
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<SessionToken> SessionTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderProduct>().ToTable("OrderProduct");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderProduct>(op => {
                op.HasKey(op => new { op.OrderID, op.ProductID });

                op.HasOne(op => op.Order).WithMany(o => o.OrderProducts).HasForeignKey(op => op.OrderID);
                op.HasOne(op => op.Product).WithMany(p => p.Orders).HasForeignKey(op => op.ProductID);
            });

            modelBuilder.Entity<UserRole>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
                ur.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            });

            modelBuilder.Entity<Order>()
                .Property(o => o.ID)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Product>()
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Seed();
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Product>().HasData(
            new Product { ID = 1, Name = "product1", Price = 100, Category = Category.Category1, AddedAt = DateTime.Parse("2021-12-22") },
            new Product { ID = 2, Name = "product2", Price = 20, Category = Category.Category1, AddedAt = DateTime.Parse("2021-12-22") },
            new Product { ID = 3, Name = "product3", Price = 25, Category = Category.Category2, AddedAt = DateTime.Parse("2021-12-23") },
            new Product { ID = 4, Name = "product4", Price = 70, Category = Category.Category3, AddedAt = DateTime.Parse("2021-12-23") },
            new Product { ID = 5, Name = "product5", Price = 65, Category = Category.Category3, AddedAt = DateTime.Parse("2021-12-22") },
            new Product { ID = 6, Name = "product6", Price = 200, Category = Category.Category1, AddedAt = DateTime.Parse("2021-12-22") },
            new Product { ID = 7, Name = "product7", Price = 55, Category = Category.Category4, AddedAt = DateTime.Parse("2021-12-23") },
            new Product { ID = 8, Name = "product8", Price = 90, Category = Category.Category4, AddedAt = DateTime.Parse("2021-12-23") },
            new Product { ID = 9, Name = "product9", Price = 120, Category = Category.Category2, AddedAt = DateTime.Parse("2021-12-22") },
            new Product { ID = 10, Name = "product10", Price = 320, Category = Category.Category3, AddedAt = DateTime.Parse("2021-12-22") }
        );
        }
    }
}