using NSubstitute;
using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.UseCases
{
    public class ApplyDiscountShould
    {
        private IShoppingCartAdministrator shoppingCartAdministrator;
        private IBaseUseCase<DiscountRequest> applyDiscountUseCase;

        [SetUp]
        public void SetUp()
        {
            shoppingCartAdministrator = Substitute.For<IShoppingCartAdministrator>();
            shoppingCartAdministrator.ApplyDiscount(Arg.Any<Discount>());

            applyDiscountUseCase = new ApplyDiscountUseCase(shoppingCartAdministrator);
        }

        [Test]
        public void AddProductToShoppingCartSuccessfully()
        {
            DiscountDTO discountDTO = new DiscountDTO
            {
                 DiscountName = "Test",
                 DiscountQuantity = 1
            };
            DiscountRequest discountRequest = new DiscountRequest(discountDTO);

            Assert.DoesNotThrow(() => applyDiscountUseCase.Execute(discountRequest));

            shoppingCartAdministrator.Received(1).ApplyDiscount(Arg.Any<Discount>());
        }

        [Test]
        public void RaiseExWhenDiscountRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => applyDiscountUseCase.Execute(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(DiscountRequest))));
        }
    }
}
