using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartAppTest.App.UseCases.AddProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartAppTest.Integration
{
    internal class ProductRepositoryShould
    {
        [Test]
        public void CreateProduct()
        {
            ShoppingCartContext context = new ShoppingCartContext();
            IProductRepository repository = new ProductRepository(context);

            ProductId productId = ProductId.Create();
            Product product = new Product(productId, ProductName.Create(), ProductPrice.Create());
            
            repository.Save(product);

            Assert.That(context.Product.First(x => x.ProductId == productId.Value()).ProductId, Is.EqualTo(productId.Value()));
        }

        [Test]
        public void UpdatePriceToExisitingProduct()
        {
            ShoppingCartContext context = new ShoppingCartContext();
            IProductRepository repository = new ProductRepository(context);

            ProductId productId = ProductId.Create();
            Product product = new Product(productId, ProductName.Create(), ProductPrice.Create());

            repository.Save(product);
            
            product.UpdatePrice(10);
            
            repository.Save(product);

            Assert.That(context.Product.First(x => x.ProductId == productId.Value()).ProductId, Is.EqualTo(productId.Value()));
        }

        [Test]
        public void RaiseErrorWhenTryingToCreateAProductWithAnProductIdThatAlreadyExists()
        {
            ShoppingCartContext context = new ShoppingCartContext();
            IProductRepository repository = new ProductRepository(context);

            ProductId productId = ProductId.Create();
            Product product = new Product(productId, ProductName.Create(), ProductPrice.Create());

            repository.Save(product);

            Product product2 = new Product(productId, ProductName.Create(), ProductPrice.Create());

            Assert.Throws<InvalidOperationException>(() => repository.Save(product2));
        }

        [Test]
        public void GetProductById()
        {
            ShoppingCartContext context = new ShoppingCartContext();
            IProductRepository repository = new ProductRepository(context);

            ProductId productId = ProductId.Create();
            Product product = new Product(productId, ProductName.Create(), ProductPrice.Create());

            repository.Save(product);

            Product returnedPproduct = repository.GetProductById(productId);

            Assert.That(returnedPproduct.GetProductId().Value(), Is.EqualTo(productId.Value()));
        }

        [Test]
        public void GetNullWhenProductDoesNotExists()
        {
            ShoppingCartContext context = new ShoppingCartContext();
            IProductRepository repository = new ProductRepository(context);

            Product returnedPproduct = repository.GetProductById(new ProductId(Guid.NewGuid().ToString()));

            Assert.That(returnedPproduct, Is.Null);
        }

        [Test]
        public void DeleteProductById()
        {
            ShoppingCartContext context = new ShoppingCartContext();
            IProductRepository repository = new ProductRepository(context);

            ProductId productId = ProductId.Create();
            Product product = new Product(productId, ProductName.Create(), ProductPrice.Create());

            repository.Save(product);
            repository.DeleteProductById(productId);

            Assert.That(context.Product.FirstOrDefault(x => x.ProductId == productId.Value()), Is.Null);
        }

        [Test]
        public void RaiseExWhenDeletetingProductAndItDoesNotExists()
        {
            ShoppingCartContext context = new ShoppingCartContext();
            IProductRepository repository = new ProductRepository(context);

            Assert.Throws<ArgumentNullException>(() => repository.DeleteProductById(ProductId.Create()));
        }
    }
}
