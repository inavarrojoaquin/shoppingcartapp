namespace ShoppingCartApp.App.Modules.ProductModule.Domain;

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

    static Product FromPrimitives(ProductData productData)
    {
        return new Product(
            new ProductId(productData.ProductId),
            new Name(productData.ProductName),
            new ProductPrice(productData.ProductPrice),
            new ProductStock(productData.Stock)
            );
    }

    public ProductData ToPrimitives()
    {
        return new ProductData
        {
            ProductId = productId.Value(),
            ProductPrice = price.Value(),
            ProductName = name.Value(),
            Stock = stock.Value()
        };
    }
    
}

public class ProductData
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
    public int Stock { get; set; }
}
