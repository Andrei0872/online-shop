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

            modelBuilder.Seed();
        }
    }

}