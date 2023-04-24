using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;

namespace ShoppingCartAppTest.App.Infrastructure;

public class ShoppingCartRepositoryShould
{
    [Test]
    public void CreateANewShoppingCart()
    {
        var shoppingCartContext = new ShoppingCartDbContext();

        var shoppingCartData = new ShoppingCartData
        {
            ShoppingCartId = Guid.NewGuid().ToString(),
            ShoppingCartName = "New ShoppingCart",
            OrderItems = new List<OrderItemData>
            {
                new OrderItemData
                {
                    Product = new ProductData
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        ProductName = "Product 1",
                        ProductPrice = 343
                    },
                    Quantity = 4,
                    OrderItemId = Guid.NewGuid().ToString()
                }
            }
        };
        shoppingCartContext.ShoppingCarts.Add(shoppingCartData);
        shoppingCartContext.SaveChanges();

        var shoppingCartRepository = new ShoppingCartRepository(shoppingCartContext);
        var shoppingCart = shoppingCartRepository.GetShoppingCartById(new ShoppingCartId(shoppingCartData.ShoppingCartId));
        
        shoppingCartContext.ShoppingCarts.Remove(shoppingCartData);
        shoppingCartContext.SaveChanges();
        Assert.NotNull(shoppingCart);
    }

    [Test]
    public void CanCreateAndDeleteAProduct()
    {
        var product1 = new Product(ProductId.Create(), new Name("Product one"), new ProductPrice(30));
        var product2 = new Product(ProductId.Create(), new Name("Product two"), new ProductPrice(40));
        var product3 = new Product(ProductId.Create(), new Name("Product three"), new ProductPrice(50));
        var shoppingCartId = new ShoppingCartId(Guid.NewGuid().ToString());
        var shoppingCart = new ShoppingCart(shoppingCartId,
            new ShoppingCartName("Toys ShoppingCart"), new List<OrderItem>());
        shoppingCart.AddProduct(product1);
        shoppingCart.AddProduct(product2);
        shoppingCart.AddProduct(product3);
        
        var dbContext = new ShoppingCartDbContext();
        var repository = new ShoppingCartRepository(dbContext);
        
        repository.Save(shoppingCart);

        shoppingCart.DeleteProduct(product2);
        repository.Save(shoppingCart);
        
        var updatedShoppingCart = repository.GetShoppingCartById(shoppingCartId);
        Assert.That(updatedShoppingCart.ToPrimitives().OrderItems.Count, Is.EqualTo(2));
    }

    [Test]
    public void UpdateProductName()
    {
        var repository = new ProductRepository(new ShoppingCartDbContext());
        var product = new Product(ProductId.Create(), new Name("Product one"), new ProductPrice(33));
        
        repository.Save(product);
        product.UpdateName(new Name("Product two"));
        repository.Save(product);

        var existingProduct = repository.GetProductById(product.GetProductId());
        Assert.That(existingProduct.ToPrimitives().ProductName, Is.EqualTo("Product two"));
    }
}