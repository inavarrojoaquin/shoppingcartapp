using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.App.Domain;

namespace ShoppingCartApp.App.Infrastructure
{
    public class ShoppingCartContext : DbContext
    {
        public DbSet<ShoppingCartData> ShoppingCart { get; set; }
        public DbSet<ProductData> Product { get; set; }
        public DbSet<OrderItemData> OrderItem { get; set; }
        public DbSet<DiscountData> Discount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1435;Database=shoppingcart;user id=sa;password=C33#8nstkKjkwQ;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO completar con el resto de atributos para cada tabla
            modelBuilder.Entity<ShoppingCartData>(x =>
            {
                x.HasKey(y => y.ShoppingCartId);
                x.HasMany(oi => oi.OrderItems);
            });
            modelBuilder.Entity<ProductData>(x => x.HasKey(y => y.ProductId));
            modelBuilder.Entity<OrderItemData>(x => x.HasKey(y => y.OrderItemId));
            modelBuilder.Entity<DiscountData>(x => x.HasKey(y => y.DiscountId));
        }
    }
}
