namespace ShoppingCartApp.DTOs
{
    public class AddProductRequest : IBaseRequest
    {
        public string Name { get; }
        public double Price { get; }
        public int Quantity { get; }
        public string ShoppingCartName { get; }
        public AddProductRequest(ProductDTO productDTO)
        {
            if(string.IsNullOrEmpty(productDTO.ProductName))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "ProductName"));
            
            Name = productDTO.ProductName;

            if (productDTO.ProductPrice < 0)
                throw new Exception(string.Format("Error: {0} can not be less than 0", "ProductPrice"));
            
            Price = productDTO.ProductPrice;

            if (productDTO.ProductQuantity < 0)
                throw new Exception(string.Format("Error: {0} can not be less than 0", "ProductQuantity"));
            
            Quantity = productDTO.ProductQuantity;

            if (string.IsNullOrEmpty(productDTO.ShoppingCartName))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "ShoppingCartName"));
            
            ShoppingCartName = productDTO.ShoppingCartName;
        }
    }
}