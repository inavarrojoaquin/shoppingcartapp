using ShoppingCartApp.App.UseCases.ApplyDiscount;
using ShoppingCartApp.DTOs;

namespace ShoppingCartAppTest.App.UseCases.ApplyDiscount
{
    internal class DiscountRequestShould
    {
        [Test]
        public void CreateProductRequestSuccessfully()
        {
            DiscountDTO discountDTO = new DiscountDTO
            {
                DiscountName = "Test",
                DiscountQuantity = 1,
            };

            DiscountRequest discountRequest = new DiscountRequest(discountDTO);

            Assert.That(string.IsNullOrEmpty(discountRequest.Name), Is.False);
            Assert.That(discountRequest.Quantity, Is.GreaterThanOrEqualTo(0));
        }

        [TestCase(null)]
        [TestCase("")]
        public void RaiseExWhenDiscountNameIsNullOrEmpty(string name)
        {
            DiscountDTO discountDTO = new DiscountDTO
            {
                DiscountName = name,
            };

            var ex = Assert.Throws<Exception>(() => new DiscountRequest(discountDTO));

            Assert.That(ex.Message, Is.EqualTo(string.Format("Error: {0} can not be null or empty", "DiscountName")));
        }

        [Test]
        public void RaiseExWhenQuantityIsLessThan0()
        {
            DiscountDTO discountDTO = new DiscountDTO
            {
                DiscountName = "Test",
                DiscountQuantity = -1
            };

            var ex = Assert.Throws<Exception>(() => new DiscountRequest(discountDTO));

            Assert.That(ex.Message, Is.EqualTo(string.Format("Error: {0} can not be less than 0", "DiscountQuantity")));
        }
    }
}
