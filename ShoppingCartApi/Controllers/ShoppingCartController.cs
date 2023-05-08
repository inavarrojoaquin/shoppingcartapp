//using Microsoft.AspNetCore.Mvc;
//using ShoppingCartApp.App.Infrastructure;
//using ShoppingCartApp.App.UseCases.AddProduct;
//using ShoppingCartApp.App.UseCases.ApplyDiscount;
//using ShoppingCartApp.App.UseCases.DeleteProduct;
//using ShoppingCartApp.App.UseCases.PrintShoppingCart;
//using ShoppingCartApp.DTOs;
//using ShoppingCartAppTest.App.UseCases.AddProduct;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace ShoppingCartApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ShoppingCartController : ControllerBase
//    {
//        private ShoppingCartDbContext context;
//        private IShoppingCartRepository shoppingCartRepository;

//        public ShoppingCartController()
//        {
//            context = new ShoppingCartDbContext();
//            shoppingCartRepository = new ShoppingCartRepository(context);
//        }

//        // GET: api/ShoppingCart
//        [HttpGet]
//        public bool Get()
//        {
//            return true;
//        }

//        // GET api/<ShoppingCartController>
//        [HttpGet]
//        public void Print(ShoppingCartDTO shoppingCartDTO)
//        {
//            PrintShoppingCartUseCase printShoppingCartUseCase = new PrintShoppingCartUseCase(shoppingCartRepository);

//            PrintShoppingCartRequest printShoppingCartRequest = new PrintShoppingCartRequest(shoppingCartDTO);
//            printShoppingCartUseCase.Execute(printShoppingCartRequest);
//        }

//        // POST api/<ShoppingCartController>
//        [HttpPost]
//        public void PostAddItem([FromBody] ProductDTO productDTO)
//        {
//            IProductRepository productRepository = new ProductRepository(context);
//            AddProductUseCase addProductUseCase = new AddProductUseCase(productRepository, shoppingCartRepository);

//            AddProductRequest productRequest = new AddProductRequest(productDTO);
//            addProductUseCase.Execute(productRequest);
//        }

//        // POST api/<ShoppingCartController>
//        [HttpPost]
//        public void PostApplyDiscount([FromBody] DiscountDTO discountDTO)
//        {
//            IDiscountRepository discountRepository = null;
//            ApplyDiscountUseCase applyDiscountUseCase = new ApplyDiscountUseCase(discountRepository, shoppingCartRepository);

//            DiscountRequest discountRequest = new DiscountRequest(discountDTO);
//            applyDiscountUseCase.Execute(discountRequest);
//        }

//        // DELETE api/<ShoppingCartController>
//        [HttpDelete]
//        public void DeleteItem([FromBody] ProductDTO productDTO)
//        {
//            IProductRepository productRepository = new ProductRepository(context);
//            DeleteProductUseCase deleteProductUseCase = new DeleteProductUseCase(productRepository, shoppingCartRepository);

//            DeleteProductRequest deleteProductRequest = new DeleteProductRequest(productDTO);
//            deleteProductUseCase.Execute(deleteProductRequest);
//        }
//    }
//}
