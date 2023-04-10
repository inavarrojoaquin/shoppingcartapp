namespace ShoppingCartApp.App.Domain
{
    public class Product
    {
        private ProductId productId;
        private ProductName name;
        private ProductPrice price;

        public Product(ProductId productId, ProductName name, ProductPrice price)
        {
            this.productId = productId;
            this.name = name;
            this.price = price;
        }

        public Product(ProductId productId)
        {
            this.productId = productId;
            this.name = ProductName.Create();
            this.price = ProductPrice.Create();            
        }

        public ProductData ToPrimitives()
        {
            return new ProductData
            {
                ProductId = this.productId.Value(),
                ProductName = this.name.Value(),
                ProductPrice = this.price.Value()
            };
        }

        public static Product FromPrimitives(ProductData data)
        {
            return new Product(new ProductId(data.ProductId),
                               new ProductName(data.ProductName),
                               new ProductPrice(data.ProductPrice));
        }

        public ProductId GetProductId()
        {
            return productId;
        }

        public double GetPrice()
        {
            return this.price.Value();
        }
    }

    public class ProductData
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}