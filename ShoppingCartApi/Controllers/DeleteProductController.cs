using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.App.UseCases.DeleteProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteProductController : ControllerBase
    {
        private IBaseUseCase<DeleteProductRequest> deleteProductUseCase;

        public DeleteProductController(IBaseUseCase<DeleteProductRequest> deleteProductUseCase)
        {
            this.deleteProductUseCase = deleteProductUseCase;
        }

        [HttpGet("alive")]
        public bool Alive()
        {
            return true;
        }

        // POST api/DeleteProduct
        [HttpPost]
        public void PostDeleteProduct([FromBody] ProductDTO productDTO)
        {
            DeleteProductRequest request = new DeleteProductRequest(productDTO);
            deleteProductUseCase.ExecuteAsync(request);
        }
    }
}
