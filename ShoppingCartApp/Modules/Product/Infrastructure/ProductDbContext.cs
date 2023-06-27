using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.Modules.ProductModule.Domain.DBClass;

namespace ShoppingCartApp.Modules.ProductModule.Infrastructure;

public class ProductDbContext : DbContext
{
    public DbSet<ProductData> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer()
            .UseSqlServer(@"Server=localhost,1435;Database=ShoppingCart;user id=sa;password=C33#8nstkKjkwQ;TrustServerCertificate=True");
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductData>()
            .HasKey(prod => prod.ProductId);
        modelBuilder.Entity<ProductData>()
            .HasData(new { ProductId = "5F056E35-B71E-4564-8F65-50DC7D8233C6", ProductName = "Product third", ProductPrice = 30.0, ProductStock = 10 });
    }
}