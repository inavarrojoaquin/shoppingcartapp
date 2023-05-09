using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShoppingCartAppTest.App.Infrastructure;

public class ShoppingCartRepositoryShould
{
    [Test]
    public void CreateANewShoppingCart()
    {
        var shoppingCartContext = new ShoppingCartDbContext();
        var shoppingCartRepository = new ShoppingCartRepository(shoppingCartContext);
        var productData1 = shoppingCartContext.Products.FirstOrDefault(p => p.ProductId == "5CBF54BA-BF19-40BF-B97D-4827A11720A2");
        var shoppingCartId = ShoppingCartId.Create();
        var shoppingCart = new ShoppingCart(shoppingCartId);
        shoppingCart.AddProduct(Product.FromPrimitives(productData1));
        shoppingCart.AddProduct(Product.FromPrimitives(productData1));
        
        shoppingCartRepository.Save(shoppingCart);

        var storedShoppingCartData = shoppingCartRepository.GetShoppingCartById(shoppingCartId).ToPrimitives();
        
        Assert.That(storedShoppingCartData.OrderItems.Count, Is.EqualTo(1));
        Assert.That(storedShoppingCartData.OrderItems[0].Quantity, Is.EqualTo(2));
    }

    [Test]
    public void CanCreateAndDeleteAProduct()
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
        repository.Save(shoppingCart);

        shoppingCart.DeleteProduct(Product.FromPrimitives(productData2));
        repository.Save(shoppingCart);

        ShoppingCart updatedShoppingCart = repository.GetShoppingCartById(shoppingCartId);
        
        var expected = shoppingCart.ToPrimitives();
        var current = updatedShoppingCart.ToPrimitives();

        Assert.That(current.ShoppingCartId, Is.EqualTo(expected.ShoppingCartId));
        Assert.That(current.ShoppingCartName, Is.EqualTo(expected.ShoppingCartName));
        Assert.That(current.OrderItems.Count, Is.EqualTo(expected.OrderItems.Count));
        Assert.That(current.OrderItems.First().OrderItemId, Is.EqualTo(expected.OrderItems.First().OrderItemId));
    }

    [Test]
    public void GetNullWhenRequestIdIsNull()
    {
        var dbContext = new ShoppingCartDbContext();
        var repository = new ShoppingCartRepository(dbContext);
        ShoppingCart shoppingCart = repository.GetShoppingCartById(null);
        
        Assert.That(shoppingCart, Is.Null);
    }

    [Test]
    public void GetNullWhenDoesNotExistsAShoppingCart()
    {
        var dbContext = new ShoppingCartDbContext();
        var repository = new ShoppingCartRepository(dbContext);
        ShoppingCart shoppingCart = repository.GetShoppingCartById(ShoppingCartId.Create());

        Assert.That(shoppingCart, Is.Null);
    }
}