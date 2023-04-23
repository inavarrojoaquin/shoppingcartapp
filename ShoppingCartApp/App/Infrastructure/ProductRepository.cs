using ShoppingCartApp.App.Domain;
using ShoppingCartApp.App.Infrastructure;
using ShoppingCartAppTest.App.UseCases.AddProduct;

namespace ShoppingCartAppTest.Integration
{
    public class ProductRepository : IProductRepository
    {
        private ShoppingCartContext context;

        public ProductRepository(ShoppingCartContext context)
        {
            this.context = context;
        }

        public Product GetProductById(ProductId id)
        {
            ProductData? productData = context.Product.FirstOrDefault(x => x.ProductId == id.Value());
            
            if (productData == null) 
                return null;

            return Product.FromPrimitives(productData);
        }

        public void Save(Product product)
        {
            ProductData entity = product.ToPrimitives();
            var state = context.Entry(entity).State;
            if (state == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                context.Product.Add(entity);
            }
            else
            {
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}