namespace ShoppingCartApp.DTOs
{
    public class DeleteProductRequest : IBaseRequest
    {
        public string Name { get; }
        public string ShoppingCartName { get; }
        public DeleteProductRequest(ProductDTO productDTO)
        {
            if (string.IsNullOrEmpty(productDTO.ProductName))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "ProductName"));

            Name = productDTO.ProductName;

            if (string.IsNullOrEmpty(productDTO.ShoppingCartName))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "ShoppingCartName"));

            ShoppingCartName = productDTO.ShoppingCartName;
        }
    }
}