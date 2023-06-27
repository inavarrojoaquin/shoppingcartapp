using ShoppingCartApp.Modules.ShoppingCartModule.Domain.DBClass;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;

namespace ShoppingCartApp.Modules.ShoppingCartModule.Domain
{
    public class Product : AggregateRoot<StockUpdated>
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

        public ProductData ToPrimitivesEvent()
        {
            return new ProductData
            {
                ProductId = productId.Value(),
                ProductName = name.Value(),
                ProductPrice = price.Value(),
                ProductStock = stock.Value()
            };
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

        public void UpdateStock(int quantity)
        {
            this.stock = new ProductStock(stock.Value() - quantity);

            // aqui se podria lanzar dos eventos uno si es agotado en caso que sea negativo y tener un handler que escuche 
            // y otro para el caso positivo
            // con el agregateroot actual no podria subscribir mas eventos por ende usar el approach de marcos con subscribe
            AddEvent(new StockUpdated { ProductData = ToPrimitivesEvent()});
        }
    }
}