using System.Runtime.CompilerServices;

namespace ShoppingCartApp
{
    public class Product : ICloneable<Product>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product Clone()
        {
            return new Product
            {
                Name = this.Name,
                Price = this.Price,
                Quantity = this.Quantity
            };
        }
    }
}