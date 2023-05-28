using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShoppingCartAppTest.App.Infrastructure;

public class ShoppingCartRepositoryShould
{
    [Test]
    public async Task CreateANewShoppingCart()
    {
        var shoppingCartContext = new ShoppingCartDbContext();
        var shoppingCartRepository = new ShoppingCartRepository(shoppingCartContext);
        var productData1 = shoppingCartContext.Products.FirstOrDefault(p => p.ProductId == "5CBF54BA-BF19-40BF-B97D-4827A11720A2");
        var shoppingCartId = ShoppingCartId.Create();
        var shoppingCart = new ShoppingCart(shoppingCartId);
        shoppingCart.AddProduct(Product.FromPrimitives(productData1));
        shoppingCart.AddProduct(Product.FromPrimitives(productData1));
        
        await shoppingCartRepository.SaveAsync(shoppingCart);

        var storedShoppingCartData = (await shoppingCartRepository.GetShoppingCartByIdAsync(shoppingCartId)).ToPrimitives();
        
        Assert.That(storedShoppingCartData.OrderItems.Count, Is.EqualTo(1));
        Assert.That(storedShoppingCartData.OrderItems[0].Quantity, Is.EqualTo(2));
    }

    [Test]
    public async Task CanCreateAndDeleteAProduct()
    {
        var shoppingCartId = new ShoppingCartId(Guid.NewGuid().ToString());
        ShoppingCart shoppingCart = new ShoppingCart(shoppingCartId,
            new ShoppingCartName("Toys ShoppingCart"), new List<OrderItem>());
        
        var dbContext = new ShoppingCartDbContext();
        var repository = new ShoppingCartRepository(dbContext);
        var productData1 = dbContext.Products.FirstOrDefault(p => p.ProductId == "5CBF54BA-BF19-40BF-B97D-4827A11720A2");
        var productData2 = dbContext.Products.FirstOrDefault(p => p.ProductId == "7478b9ae-2e05-4c6d-afb1-3b8934edc699");
        shoppingCart.AddProduct(Product.FromPrimitives(productData1));
        shoppingCart.AddProduct(Product.FromPrimitives(productData2));
        repository.SaveAsync(shoppingCart);

        shoppingCart.DeleteProduct(Product.FromPrimitives(productData2));
        repository.SaveAsync(shoppingCart);

        ShoppingCart updatedShoppingCart = await repository.GetShoppingCartByIdAsync(shoppingCartId);
        
        var expected = shoppingCart.ToPrimitives();
        var current = updatedShoppingCart.ToPrimitives();

        Assert.That(current.ShoppingCartId, Is.EqualTo(expected.ShoppingCartId));
        Assert.That(current.ShoppingCartName, Is.EqualTo(expected.ShoppingCartName));
        Assert.That(current.OrderItems.Count, Is.EqualTo(expected.OrderItems.Count));
        Assert.That(current.OrderItems.First().OrderItemId, Is.EqualTo(expected.OrderItems.First().OrderItemId));
    }

    [Test]
    public async Task GetNullWhenRequestIdIsNull()
    {
        var dbContext = new ShoppingCartDbContext();
        var repository = new ShoppingCartRepository(dbContext);
        ShoppingCart shoppingCart = await repository.GetShoppingCartByIdAsync(null);
        
        Assert.That(shoppingCart, Is.Null);
    }

    [Test]
    public async Task GetNullWhenDoesNotExistsAShoppingCart()
    {
        var dbContext = new ShoppingCartDbContext();
        var repository = new ShoppingCartRepository(dbContext);
        ShoppingCart shoppingCart = await repository.GetShoppingCartByIdAsync(ShoppingCartId.Create());

        Assert.That(shoppingCart, Is.Null);
    }

    [Test]
    public async Task IncreaseOrderItemQuantityIfProductExists()
    {
        var dbContext = new ShoppingCartDbContext();
        var repository = new ShoppingCartRepository(dbContext);
        var shoppingCartId = new ShoppingCartId("9DFB1807-521F-4F21-B9E2-408F1A03B853");
        var productId = new ProductId("5CBF54BA-BF19-40BF-B97D-4827A11720A2");
        var product = new Product(productId, new Name("Product one"), new ProductPrice(20));
        var shoppingCart = await repository.GetShoppingCartByIdAsync(shoppingCartId); 
        if (shoppingCart == null)
        {
            shoppingCart = new ShoppingCart(shoppingCartId);
            shoppingCart.AddProduct(product);
            repository.SaveAsync(shoppingCart);
        }
        shoppingCart.AddProduct(product);
        repository.SaveAsync(shoppingCart);
    }
}