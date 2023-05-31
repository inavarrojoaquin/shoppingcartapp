﻿using ShoppingCartApp.App.Modules.ShoppingCartModule.Domain;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.Shared.Events
{
    public class ShoppingCartClosed : IDomainEvent
    {
        public ShoppingCartData ShoppingCartData { get; set; }
    }
}