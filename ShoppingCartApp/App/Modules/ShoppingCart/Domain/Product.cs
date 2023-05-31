namespace ShoppingCartApp.App.Modules.ShoppingCartModule.Domain
{
    public class Product
    {
        private ProductId productId;
        private Name name;
        private ProductPrice price;
        private ProductData productData;

        public Product(ProductId productId, Name name, ProductPrice price)
        {
            this.productId = productId;
            this.name = name;
            this.price = price;
            productData = new ProductData();
        }

        public Product(ProductId productId)
        {
            this.productId = productId;
            name = Name.Create();
            price = ProductPrice.Create();
        }

        public ProductData ToPrimitives()
        {
            productData.ProductId = productId.Value();
            productData.ProductName = name.Value();
            productData.ProductPrice = price.Value();
            return productData;
        }

        public static Product FromPrimitives(ProductData data)
        {
            return new Product(new ProductId(data.ProductId),
                               new Name(data.ProductName),
                               new ProductPrice(data.ProductPrice));
        }

        public ProductId GetProductId()
        {
            return productId;
        }

        public double GetPrice()
        {
            return price.Value();
        }

        public void UpdateName(Name name)
        {
            this.name = name;
        }
    }

    public class ProductData
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}