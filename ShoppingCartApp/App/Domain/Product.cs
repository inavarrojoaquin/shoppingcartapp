namespace ShoppingCartApp.App.Domain
{
    public class Product
    {
        private ProductId productId;
        private ProductName productName;
        private ProductPrice productPrice;
        private ProductData productData;
        public Product(ProductId productId, ProductName name, ProductPrice price)
        {
            this.productId = productId;
            this.productName = name;
            this.productPrice = price;
            this.productData = new ProductData();
        }

        public ProductData ToPrimitives()
        {
            productData.ProductId = this.productId.Value();
            productData.ProductName = this.productName.Value();
            productData.ProductPrice = this.productPrice.Value();

            return productData;
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
            return this.productPrice.Value();
        }

        public void UpdatePrice(int price)
        {
            this.productPrice = new ProductPrice(price);
        }
    }

    public class ProductData
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}