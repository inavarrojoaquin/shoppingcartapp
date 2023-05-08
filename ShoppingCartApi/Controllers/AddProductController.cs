using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartAppTest.App.UseCases.AddProduct;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProductController : ControllerBase
    {
        private readonly ICommandBus commandBus;

        public AddProductController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpGet("{id}")]
        public IEnumerable<string> Get(string id)
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<ShoppingCartController>
        [HttpPost]
        public async Task PostAddItem([FromBody] ProductDTO productDTO)
        {
            AddProductCommand command = new AddProductCommand
            {
                productDTO = productDTO
            };
            await commandBus.SendAsync(command);
        }
    }
}
