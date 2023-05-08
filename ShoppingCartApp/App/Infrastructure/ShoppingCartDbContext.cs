using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShoppingCartApp.App.Domain;

namespace ShoppingCartApp.App.Infrastructure;

public class ShoppingCartDbContext : DbContext
{
    public DbSet<ShoppingCartData> ShoppingCarts { get; set; }
    public DbSet<OrderItemData> OrderItems { get; set; }
    public DbSet<ProductData> Products { get; set; }
    
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

        modelBuilder.Entity<ProductData>()
                    .HasData(new { ProductId = "5CBF54BA-BF19-40BF-B97D-4827A11720A2", ProductName = "Product one", ProductPrice = 30.0 });
        modelBuilder.Entity<ProductData>()
                    .HasData(new { ProductId = "7478b9ae-2e05-4c6d-afb1-3b8934edc699", ProductName = "Product two", ProductPrice = 40.0 });
    }

    public void StartTransaction()
    {
        Database.BeginTransaction();
    }

    public void RollbackTransaction()
    {
        Database.RollbackTransaction();
    }

    public void EndTransaction()
    {
        Database.CommitTransaction();
    }
}