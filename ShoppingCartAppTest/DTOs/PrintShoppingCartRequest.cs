namespace ShoppingCartApp.DTOs
{
    public class PrintShoppingCartRequest : IBaseRequest
    {
        public string ShoppingCartName { get; }
        public PrintShoppingCartRequest(ShoppingCartDTO shoppingCartDTO)
        {
            if (string.IsNullOrEmpty(shoppingCartDTO.ShoppingCartName))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "ShoppingCartName"));

            ShoppingCartName = shoppingCartDTO.ShoppingCartName;
        }
    }
}