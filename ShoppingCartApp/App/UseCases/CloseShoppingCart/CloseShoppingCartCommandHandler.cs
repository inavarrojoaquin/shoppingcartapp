﻿using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApp.App.UseCases.CloseShoppingCart
{
    public class CloseShoppingCartCommandHandler : ICommandHandler<CloseShoppingCartCommand>
    {
        private readonly IBaseUseCase<CloseShoppingCartRequest> useCase;

        public CloseShoppingCartCommandHandler(IBaseUseCase<CloseShoppingCartRequest> useCase)
        {
            this.useCase = useCase;
        }
        public async Task Handle(CloseShoppingCartCommand command)
        {
            await useCase.ExecuteAsync(new CloseShoppingCartRequest(command.ShoppingCartDTO));
        }
    }
}
