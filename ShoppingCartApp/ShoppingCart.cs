using System.Text;

namespace ShoppingCartApp
{
    internal class ShoppingCart : IShoppingCart
    {
        private Products products;
        private Discounts discounts;

        // Propducts no deberia saber de Discount
        // En esta clase shoppingCart deberia gestionar el vinculo entre Products y Discounts
        // ShoppingCartController -> recibe productDto -> UseCase(requestDto) -> dentro del useCase creo el objeto de Dominio real en base a la requestDto
        // Usar NUnit.Substitute (outside-in -> Londres School), comienzo por los UseCases y luego el Controller
        // inside-out -> Chicago School
        // Recibo una shoppingCartTarget y un productDTO en la request
        // Si añado un prod se crea una shoppingcart en caso q no exista, sino le agrego el prod a esa shoppingCart 
        // El useCase crea el objeto de dominio (ValueObject)
        // Use cases, cada addProduct, etc sera un usecase(request)
        // ValueObjects
        // PrintShoppingCart deberia devolver un json
        public ShoppingCart()
        {
            products = new Products();
        }

        public void AddProduct(Product product)
        {
            products.AddProduct(product);
        }

        public void ApplyDiscount(string discount)
        {
            products.ApplyDiscount(discount);
        }

        public void DeleteProduct(Product product)
        {
            products.DeleteProduct(product);
        }

        public string PrintShoppingCart()
        {
            StringBuilder shoppingCartBuilder = new();
            shoppingCartBuilder.AppendLine(products.PrintProducts());
            shoppingCartBuilder.AppendLine(products.PrintPromotion());
            shoppingCartBuilder.AppendLine(products.PrintTotalOfProducts());
            shoppingCartBuilder.AppendLine(products.PrintTotalPrice());

            return shoppingCartBuilder.ToString();
        }
    }
}