using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProductController : ControllerBase
    {
        private IBaseUseCase<AddProductRequest> addProductUseCase;

        public AddProductController(IBaseUseCase<AddProductRequest> addProductUseCase)
        {
            this.addProductUseCase = addProductUseCase;
        }

        [HttpGet]
        public bool Alive()
        {
            return true;
        }

        // POST api/AddProduct
        [HttpPost]
        public void PostAddItem([FromBody] ProductDTO productDTO)
        {
            AddProductRequest productRequest = new AddProductRequest(productDTO);
            addProductUseCase.Execute(productRequest);
        }
    }
}
