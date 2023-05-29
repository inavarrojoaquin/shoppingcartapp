using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.App.UseCases.CloseShoppingCart;
using ShoppingCartApp.App.UseCases.DeleteProduct;
using ShoppingCartApp.App.UseCases.PrintShoppingCart;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryBus queryBus;

        public ShoppingCartController(ICommandBus commandBus, IQueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }

        // POST api/ShoppingCart/product/add
        [HttpPost("product/add")]
        public async Task PostAddProduct([FromBody] ProductDTO productDTO)
        {
            AddProductCommand command = new AddProductCommand { ProductDTO = productDTO };
            await commandBus.SendAsync(command);
        }

        // POST api/ShoppingCart/product/delete
        [HttpPost("product/delete")]
        public async Task PostDeleteProduct([FromBody] ProductDTO productDTO)
        {
            DeleteProductCommand command = new DeleteProductCommand { ProductDTO = productDTO };
            await commandBus.SendAsync(command);
        }

        // GET api/ShoppingCart/print/{shoppingCartId}
        [HttpGet("print/{shoppingCartId}")]
        public async Task<string> Print(string shoppingCartId)
        {
            PrintShoppingCartQuery query = new PrintShoppingCartQuery { ShoppingCartId = shoppingCartId };
            return await queryBus.SendAsync<PrintShoppingCartQuery, string>(query);
        }

        // POST api/ShoppingCart/product/add
        [HttpPost("close")]
        public async Task PostClose([FromBody] ShoppingCartDTO shoppingCartDTO)
        {
            CloseShoppingCartCommand command = new CloseShoppingCartCommand { ShoppingCartDTO = shoppingCartDTO };
            await commandBus.SendAsync(command);
        }
    }
}
