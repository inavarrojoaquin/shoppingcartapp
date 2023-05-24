using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProductController : ControllerBase
    {
        private readonly ICommandBus commandBus;
        //private IBaseUseCase<AddProductRequest> addProductUseCase;

        public AddProductController(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        [HttpGet("alive")]
        public bool Alive()
        {
            return true;
        }

        // POST api/AddProduct
        [HttpPost]
        public async Task PostAddItem([FromBody] ProductDTO productDTO)
        {
            //AddProductRequest productRequest = new AddProductRequest(productDTO);
            //addProductUseCase.Execute(productRequest);

            AddProductCommand addProductCommand = new AddProductCommand { ProductDTO = productDTO};
            await commandBus.SendAsync(addProductCommand);
        }
    }
}
