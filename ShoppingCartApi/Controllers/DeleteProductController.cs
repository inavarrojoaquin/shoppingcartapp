using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.App.UseCases.DeleteProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteProductController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public DeleteProductController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        // POST api/DeleteProduct
        [HttpPost]
        public async Task PostDeleteProduct([FromBody] ProductDTO productDTO)
        {
            await commandBus.SendAsync(new DeleteProductCommand { ProductDTO = productDTO});
        }
    }
}
