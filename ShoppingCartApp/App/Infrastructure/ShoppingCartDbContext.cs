using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShoppingCartApp.App.Domain;

namespace ShoppingCartApp.App.Infrastructure;

public class ShoppingCartDbContext : DbContext
{
    public DbSet<ShoppingCartData> ShoppingCarts { get; set; }
    public DbSet<OrderItemData> OrderItems { get; set; }
    public DbSet<ProductData> Products { get; set; }

    public void BeginTransaction()
    {
        Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        Database.RollbackTransaction();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer()
            .UseSqlServer(@"Server=localhost,1435;Database=ShoppingCart;user id=sa;password=C33#8nstkKjkwQ;TrustServerCertificate=True");
        optionsBuilder.EnableSensitiveDataLogging();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCartData>()
            .HasKey(sc => sc.ShoppingCartId);
        modelBuilder.Entity<OrderItemData>()
            .HasKey(oi => oi.OrderItemId);
        modelBuilder.Entity<OrderItemData>()
            .HasOne(oi => oi.ShoppingCartData)
            .WithMany(oi => oi.OrderItems)
            .HasForeignKey(oi => oi.ShoppingCartId);
        modelBuilder.Entity<ProductData>()
            .HasKey(prod => prod.ProductId);
    }
}