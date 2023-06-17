using ShoppingCartApp.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Modules.ShoppingCartModule.Infrastructure;

namespace ShoppingCartAppTest.Modules.ShoppingCartModule.Infrastructure
{
    internal class ProductRepositoryShould
    {
        [Test]
        public void GetExistingProduct()
        {
            ShoppingCartDbContext context = new ShoppingCartDbContext();
            SMProductRepository productRepository = new SMProductRepository(context);

            ProductId id = new ProductId("5CBF54BA-BF19-40BF-B97D-4827A11720A2");
            Product targetProduct = productRepository.GetProductById(id);

            ProductData expectedProductData = context.Products
                .First(p => p.ProductId == "5CBF54BA-BF19-40BF-B97D-4827A11720A2");

            var expected = expectedProductData;
            var current = targetProduct.ToPrimitives();

            Assert.That(current.ProductId.ToUpper(), Is.EqualTo(expected.ProductId));
            Assert.That(current.ProductName, Is.EqualTo(expected.ProductName));
            Assert.That(current.ProductPrice, Is.EqualTo(expected.ProductPrice));
        }

        [Test]
        public void GetNullWhenRequestIdIsNull()
        {
            var context = new ShoppingCartDbContext();
            SMProductRepository productRepository = new SMProductRepository(context);

            Product current = productRepository.GetProductById(null);

            Assert.That(current, Is.Null);
        }

        [Test]
        public void GetNullWhenProductDoesNotExists()
        {
            ShoppingCartDbContext context = new ShoppingCartDbContext();
            SMProductRepository productRepository = new SMProductRepository(context);

            Product current = productRepository.GetProductById(ProductId.Create());

            Assert.That(current, Is.Null);
        }

        [Test]
        public void UpdateProductName()
        {
            var repository = new SMProductRepository(new ShoppingCartDbContext());
            var product = new Product(ProductId.Create(), new Name("Product one"), new ProductPrice(33));

            repository.Save(product);
            product.UpdateName(new Name("Product renamed"));
            repository.Save(product);

            var existingProduct = repository.GetProductById(product.GetProductId());
            Assert.That(existingProduct.ToPrimitives().ProductName, Is.EqualTo("Product renamed"));
        }
    }
}
