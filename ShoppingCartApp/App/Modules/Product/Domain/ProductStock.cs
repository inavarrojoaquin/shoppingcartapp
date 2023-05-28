namespace ShoppingCartApp.App.Domain;

public class ProductStock
{
    private readonly int quantity;

    public ProductStock(int quantity)
    {
        if (quantity < 0)
            throw new Exception(string.Format("Error: {0} can not be less than 0", "Product stock"));

        this.quantity = quantity;
    }

    public int Value() => quantity;
}