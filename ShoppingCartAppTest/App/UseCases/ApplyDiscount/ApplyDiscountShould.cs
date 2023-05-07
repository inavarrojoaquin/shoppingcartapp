﻿using NSubstitute;
using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.App.UseCases.ApplyDiscount;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartAppTest.App.UseCases.ApplyDiscount
{
    public class ApplyDiscountShould
    {
        private IDiscountRepository discountRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private IBaseUseCase<DiscountRequest> applyDiscountUseCase;

        [SetUp]
        public void SetUp()
        {
            discountRepository = Substitute.For<IDiscountRepository>();
            shoppingCartRepository = Substitute.For<IShoppingCartRepository>();
            applyDiscountUseCase = new ApplyDiscountUseCase(discountRepository, shoppingCartRepository);
        }

        [Test]
        public void ApplyDiscountToShoppingCart()
        {
            DiscountId discountId = DiscountId.Create();
            Quantity quantity = Quantity.Create();
            Discount discount = new Discount(discountId, Name.Create(), quantity);
            discountRepository.GetDiscountById(Arg.Any<DiscountId>()).Returns(discount);
            ShoppingCartId shoppingCartId = ShoppingCartId.Create();
            ShoppingCart shoppingCart = Substitute.For<ShoppingCart>(shoppingCartId);
            shoppingCart.AddProduct(new Product(ProductId.Create(), Name.Create(), new ProductPrice(10)));
            shoppingCartRepository.GetShoppingCartById(Arg.Any<ShoppingCartId>()).Returns(shoppingCart);

            DiscountDTO discountDTO = new DiscountDTO
            {
                DiscountId = discountId.Value(),
                ShoppingCartId = shoppingCartId.Value(),
                DiscountQuantity = quantity.Value(),
            };

            applyDiscountUseCase.Execute(new DiscountRequest(discountDTO));

            discountRepository.Received(1).GetDiscountById(Arg.Any<DiscountId>());
            shoppingCartRepository.Received(1).GetShoppingCartById(Arg.Any<ShoppingCartId>());
            shoppingCartRepository.Received(1).Save(Arg.Any<ShoppingCart>());
        }

        [Test]
        public void RaiseExWhenApplyDiscountToEmptyShoppingCart()
        {
            DiscountId discountId = DiscountId.Create();
            Quantity quantity = Quantity.Create();
            Discount discount = new Discount(discountId, Name.Create(), quantity);
            discountRepository.GetDiscountById(Arg.Any<DiscountId>()).Returns(discount);
            ShoppingCartId shoppingCartId = ShoppingCartId.Create();
            ShoppingCart shoppingCart = Substitute.For<ShoppingCart>(shoppingCartId);
            shoppingCartRepository.GetShoppingCartById(Arg.Any<ShoppingCartId>()).Returns(shoppingCart);

            DiscountDTO discountDTO = new DiscountDTO
            {
                DiscountId = discountId.Value(),
                ShoppingCartId = shoppingCartId.Value(),
                DiscountQuantity = quantity.Value(),
            };

            var ex = Assert.Throws<Exception>(() => applyDiscountUseCase.Execute(new DiscountRequest(discountDTO)));

            Assert.That(ex.Message, Does.Contain("Error: Can not apply discount to an empty ShoppingCart"));
        }

        [Test]
        public void RaiseExWhenDiscountRequestIsNull()
        {
            var ex = Assert.Throws<Exception>(() => applyDiscountUseCase.Execute(null));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: {0} can't be null", typeof(DiscountRequest))));
        }

        [Test]
        public void RaiseExWhenDiscountDoesNotExists()
        {
            discountRepository.GetDiscountById(Arg.Any<DiscountId>()).Returns(x => null);
            DiscountDTO discountDTO = new DiscountDTO
            {
                DiscountId = DiscountId.Create().Value(),
                ShoppingCartId = ShoppingCartId.Create().Value(),
                DiscountQuantity = Quantity.Create().Value(),
            };

            var ex = Assert.Throws<Exception>(() => applyDiscountUseCase.Execute(new DiscountRequest(discountDTO)));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: There is no discount for id: {0}", discountDTO.DiscountId)));
        }

        [Test]
        public void RaiseExWhenShoppingCartDoesNotExists()
        {
            DiscountId discountId = DiscountId.Create();
            Quantity quantity = Quantity.Create();
            Discount discount = new Discount(discountId, Name.Create(), quantity);
            discountRepository.GetDiscountById(Arg.Any<DiscountId>()).Returns(discount);
            shoppingCartRepository.GetShoppingCartById(Arg.Any<ShoppingCartId>()).Returns(x => null);

            DiscountDTO discountDTO = new DiscountDTO
            {
                DiscountId = discountId.Value(),
                ShoppingCartId = ShoppingCartId.Create().Value(),
                DiscountQuantity = quantity.Value(),
            };

            var ex = Assert.Throws<Exception>(() => applyDiscountUseCase.Execute(new DiscountRequest(discountDTO)));

            Assert.That(ex.Message, Does.Contain(string.Format("Error: There is no shoppingCart for id: {0}", discountDTO.ShoppingCartId)));
        }
    }
}