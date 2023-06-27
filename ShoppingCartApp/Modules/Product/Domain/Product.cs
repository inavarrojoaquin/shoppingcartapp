using ShoppingCartApp.Modules.ProductModule.Domain.DBClass;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.Events;

namespace ShoppingCartApp.Modules.ProductModule.Domain;

public class Product : AggregateRoot<ProductStockUpdated>
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

    public static Product FromPrimitives(ProductData productData)
    {
        return new Product(
            new ProductId(productData.ProductId),
            new Name(productData.ProductName),
            new ProductPrice(productData.ProductPrice),
            new ProductStock(productData.ProductStock));
    }

    public ProductData ToPrimitives()
    {
        return new ProductData
        {
            ProductId = productId.Value(),
            ProductPrice = price.Value(),
            ProductName = name.Value(),
            ProductStock = stock.Value()
        };
    }

    public void UpdateStock(int quantity)
    {
        this.stock = new ProductStock(stock.Value() - quantity);

        // aqui se podria lanzar dos eventos uno si es agotado en caso que sea negativo y tener un handler que escuche 
        // y otro para el caso positivo
        // con el agregateroot actual no podria subscribir mas eventos por ende usar el approach de marcos con subscribe
        AddEvent(new ProductStockUpdated { ProductData = this.productData });
    }
}
