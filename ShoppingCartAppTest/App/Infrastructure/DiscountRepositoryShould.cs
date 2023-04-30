using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartAppTest.App.Infrastructure
{
    internal class DiscountRepositoryShould
    {
        [Test]
        public void GetExistingDiscount() 
        {
            ShoppingCartDbContext context = new ShoppingCartDbContext();
            IDiscountRepository discountRepository = new DiscountRepository(context);

            // TODO: probar el Guid.ParseExact para que no lo convierta en minisculas
            DiscountId id = new DiscountId("5CBF54BA-BF19-40BF-B97D-4827A11720A2");
            Discount targetDiscount = discountRepository.GetDiscountById(id);

            ProductData expectedProductData = context.Products
                .First(p => p.ProductId == "5CBF54BA-BF19-40BF-B97D-4827A11720A2");

            var expected = JsonSerializer.Serialize(expectedProductData);
            var current = JsonSerializer.Serialize(targetDiscount.ToPrimitives());

            Assert.That(expected, Is.EqualTo(current));
        }
    }
}
