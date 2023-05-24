using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartApp.App.UseCases.AddProduct;
using ShoppingCartApp.App.UseCases.ApplyDiscount;
using ShoppingCartApp.App.UseCases.DeleteProduct;
using ShoppingCartApp.App.UseCases.PrintShoppingCart;
using ShoppingCartApp.DTOs;
using ShoppingCartApp.Shared.Domain;
using ShoppingCartAppTest.App.UseCases.AddProduct;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ShoppingCartDbContext context;
        private IShoppingCartRepository shoppingCartRepository;
        private readonly IQueryBus queryBus;

        public ShoppingCartController(IQueryBus queryBus)
        {
            context = new ShoppingCartDbContext();
            shoppingCartRepository = new ShoppingCartRepository(context);
            this.queryBus = queryBus;
        }

        // GET api/<ShoppingCartController>
        [HttpGet("print/{shoppingCartId}")]
        public async Task<string> Print(string shoppingCartId)
        {
            //PrintShoppingCartUseCase printShoppingCartUseCase = new PrintShoppingCartUseCase(shoppingCartRepository);

            //PrintShoppingCartRequest printShoppingCartRequest = new PrintShoppingCartRequest(new ShoppingCartDTO { ShoppingCartId = shoppingCartId });
            PrintShoppingCartQuery query = new PrintShoppingCartQuery { ShoppingCartId = shoppingCartId };

            return await queryBus.SendAsync<PrintShoppingCartQuery,string>(query);
            
            //return await printShoppingCartUseCase.ExecuteAsync(printShoppingCartRequest);
        }

        //// POST api/<ShoppingCartController>
        [HttpPost]
        public void PostAddProduct([FromBody] ProductDTO productDTO)
        {
            IProductRepository productRepository = new ProductRepository(context);
            AddProductUseCase addProductUseCase = new AddProductUseCase(productRepository, shoppingCartRepository);

            AddProductRequest productRequest = new AddProductRequest(productDTO);
            addProductUseCase.ExecuteAsync(productRequest);
        }

        //// POST api/<ShoppingCartController>
        //[HttpPost]
        //public void PostApplyDiscount([FromBody] DiscountDTO discountDTO)
        //{
        //    IDiscountRepository discountRepository = null;
        //    ApplyDiscountUseCase applyDiscountUseCase = new ApplyDiscountUseCase(discountRepository, shoppingCartRepository);

        //    DiscountRequest discountRequest = new DiscountRequest(discountDTO);
        //    applyDiscountUseCase.Execute(discountRequest);
        //}

        //// DELETE api/<ShoppingCartController>
        //[HttpDelete]
        //public void DeleteItem([FromBody] ProductDTO productDTO)
        //{
        //    IProductRepository productRepository = new ProductRepository(context);
        //    DeleteProductUseCase deleteProductUseCase = new DeleteProductUseCase(productRepository, shoppingCartRepository);

        //    DeleteProductRequest deleteProductRequest = new DeleteProductRequest(productDTO);
        //    deleteProductUseCase.Execute(deleteProductRequest);
        //}
    }
}
