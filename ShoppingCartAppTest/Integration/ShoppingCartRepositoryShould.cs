using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartAppTest.Integration
{
    internal class ShoppingCartRepositoryShould
    {
        [Test]
        public void CreateShoppingCart()
        {
            ShoppingCartContext context = new ShoppingCartContext();
            IShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository(context);

            // crear productos 

            ShoppingCartId shoppingCartId = ShoppingCartId.Create();
            ShoppingCart shoppingCart = new ShoppingCart(shoppingCartId);


            ProductData? productData1 = context.Product.FirstOrDefault(x => x.ProductId == "860794c4-6198-46ad-8659-4d38beaa24a2");
            Product product1 = Product.FromPrimitives(productData1);
            //Product product2 = Product.FromPrimitives(context.Product.FirstOrDefault(x => x.ProductId == "967c9282-50cb-41ca-8842-097e08f53f5a"));
            //Product product3 = Product.FromPrimitives(context.Product.FirstOrDefault(x => x.ProductId == "acc38728-255f-45cd-a7fa-4feba10d5706"));

            shoppingCart.AddProduct(product1);
            //shoppingCart.AddProduct(product2);
            //shoppingCart.AddProduct(product3);

            shoppingCartRepository.Save(shoppingCart);

            Assert.That(shoppingCart.ToPrimitives(), Is.EqualTo(shoppingCartRepository.GetShoppingCartById(shoppingCartId).ToPrimitives()));
        }
    }
}
