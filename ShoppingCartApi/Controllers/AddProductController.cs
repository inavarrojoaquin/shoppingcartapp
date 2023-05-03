using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.UseCases;
using ShoppingCartAppTest.App.UseCases.AddProduct;

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProductController : ControllerBase
    {
        private IBaseUseCase<AddProductRequest> addProductUseCase;

        //public AddProductController()
        //{
        //    ShoppingCartDbContext context = new ShoppingCartDbContext();
        //    IShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository(context);
        //    IProductRepository productRepository = new ProductRepository(context);
            
        //    addProductUseCase = new AddProductUseCase(productRepository, shoppingCartRepository);
        //}

        public AddProductController(IBaseUseCase<AddProductRequest> addProductUseCase)
        {
            this.addProductUseCase = addProductUseCase;
        }

        [HttpGet("{id}")]
        public IEnumerable<string> Get(string id)
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<ShoppingCartController>
        [HttpPost]
        public void PostAddItem([FromBody] ProductDTO productDTO)
        {
            AddProductRequest productRequest = new AddProductRequest(productDTO);
            addProductUseCase.Execute(productRequest);
        }
    }
}
