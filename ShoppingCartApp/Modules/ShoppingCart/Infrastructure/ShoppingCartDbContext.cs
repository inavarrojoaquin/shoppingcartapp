using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass;

namespace ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;

public class ShoppingCartDbContext : DbContext
{
    public DbSet<ShoppingCartData> ShoppingCarts { get; set; }
    public DbSet<ProductData> Products { get; set; }
    public DbSet<OrderItemData> OrderItems { get; set; }

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
        
        modelBuilder.Entity<ProductData>()
            .HasKey(prod => prod.ProductId);
        
        modelBuilder.Entity<OrderItemData>()
            .HasKey(oi => new { oi.OrderItemId, oi.ProductId });
        modelBuilder.Entity<OrderItemData>()
            .HasOne(x => x.ShoppingCart)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.ShoppingCartId)
            .IsRequired();
        modelBuilder.Entity<OrderItemData>()
            .HasOne(x => x.Product)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.ProductId)
            .IsRequired();

        modelBuilder.Entity<ProductData>()
            .HasData(new ProductData { ProductId = "5CBF54BA-BF19-40BF-B97D-4827A11720A2", ProductName = "Product one", ProductPrice = 30.0 });
        modelBuilder.Entity<ProductData>()
            .HasData(new ProductData { ProductId = "7478b9ae-2e05-4c6d-afb1-3b8934edc699", ProductName = "Product two", ProductPrice = 40.0 });
    }

    public async Task StartTransactionAsync()
    {
        await Database.BeginTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await Database.RollbackTransactionAsync();
    }

    public async Task EndTransactionAsync()
    {
        await Database.CommitTransactionAsync();
    }
}