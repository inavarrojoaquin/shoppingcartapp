using ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass;

namespace ShoppingCartApp.Modules.ShoppingCartModule.Domain
{
    public class Product
    {
        private ProductId productId;
        private Name name;
        private ProductPrice price;
        private ProductStock stock;
        private ProductData productData;

        public Product(ProductId productId, Name name, ProductPrice price, ProductStock stock)
        {
            this.productId = productId;
            this.name = name;
            this.price = price;
            this.stock = stock;
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
            productData.ProductStock = stock.Value();
            return productData;
        }

        public static Product FromPrimitives(ProductData productData)
        {
            return new Product(new ProductId(productData.ProductId),
                               new Name(productData.ProductName),
                               new ProductPrice(productData.ProductPrice),
                               new ProductStock(productData.ProductStock));
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
}