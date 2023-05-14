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
        private readonly IQueryBus queryBus;
        private ShoppingCartDbContext context;
        private IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartController(IQueryBus queryBus)
        {
            this.queryBus = queryBus;
            context = new ShoppingCartDbContext();
            shoppingCartRepository = new ShoppingCartRepository(context);
        }

        // GET api/<ShoppingCartController>
        [HttpGet("{shoppingCartId}")]
        public async Task<string> Print(string shoppingCartId)
        {
            var printShoppingCartQuery = new PrintShoppingCartQuery
            {
                ShoppingCartId = shoppingCartId
            };
            return await queryBus.SendAsync<PrintShoppingCartQuery, string>(printShoppingCartQuery);
        }

        // POST api/<ShoppingCartController>
        [HttpPost]
        public void PostApplyDiscount([FromBody] DiscountDTO discountDTO)
        {
            IDiscountRepository discountRepository = null;
            ApplyDiscountUseCase applyDiscountUseCase = new ApplyDiscountUseCase(discountRepository, shoppingCartRepository);

            DiscountRequest discountRequest = new DiscountRequest(discountDTO);
            applyDiscountUseCase.Execute(discountRequest);
        }

        // DELETE api/<ShoppingCartController>
        [HttpDelete]
        public void DeleteItem([FromBody] ProductDTO productDTO)
        {
            IProductRepository productRepository = new ProductRepository(context);
            DeleteProductUseCase deleteProductUseCase = new DeleteProductUseCase(productRepository, shoppingCartRepository);

            DeleteProductRequest deleteProductRequest = new DeleteProductRequest(productDTO);
            deleteProductUseCase.Execute(deleteProductRequest);
        }
    }
}
