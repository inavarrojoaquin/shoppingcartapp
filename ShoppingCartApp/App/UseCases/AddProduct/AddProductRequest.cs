﻿using ShoppingCartApp.App.Domain;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApp.App.UseCases.AddProduct
{
    public class AddProductRequest : IBaseRequest
    {
        public ShoppingCartId ShoppingCartId { get; }
        public ProductId ProductId { get; }

        public AddProductRequest(ProductDTO productDTO)
        {
            ProductId = new ProductId(productDTO.ProductId);
            ShoppingCartId = new ShoppingCartId(productDTO.ShoppingCartId);
        }
    }
}