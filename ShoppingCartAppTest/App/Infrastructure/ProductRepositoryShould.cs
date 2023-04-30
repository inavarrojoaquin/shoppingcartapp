using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingCartAppTest.App.Infrastructure
{
    internal class ProductRepositoryShould
    {
        [Test]
        public void GetExistingProduct()
        {
            ShoppingCartDbContext context = new ShoppingCartDbContext();
            ProductRepository productRepository = new ProductRepository(context);

            // TODO: probar el Guid.ParseExact para que no lo convierta en minisculas
            ProductId id = new ProductId("5CBF54BA-BF19-40BF-B97D-4827A11720A2");
            Product targetProduct = productRepository.GetProductById(id);

            ProductData expectedProductData = context.Products
                .First(p => p.ProductId == "5CBF54BA-BF19-40BF-B97D-4827A11720A2");

            var expected = JsonSerializer.Serialize(expectedProductData);
            var current = JsonSerializer.Serialize(targetProduct.ToPrimitives());

            Assert.That(expected, Is.EqualTo(current));
        }

        [Test]
        public void GetNullWhenProductDoesNotExists() 
        {
            ShoppingCartDbContext context = new ShoppingCartDbContext();
            ProductRepository productRepository = new ProductRepository(context);

            ProductId id = new ProductId("5CBF54BA-9999-9999-9999-4827A11720A2");
            Product current = productRepository.GetProductById(id);

            Assert.That(current, Is.Null);
        }

        [Test]
        public void UpdateProductName()
        {
            var repository = new ProductRepository(new ShoppingCartDbContext());
            var product = new Product(ProductId.Create(), new Name("Product one"), new ProductPrice(33));

            repository.Save(product);
            product.UpdateName(new Name("Product renamed"));
            repository.Save(product);

            var existingProduct = repository.GetProductById(product.GetProductId());
            Assert.That(existingProduct.ToPrimitives().ProductName, Is.EqualTo("Product renamed"));
        }
    }
}
