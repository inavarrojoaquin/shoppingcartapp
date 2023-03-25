namespace ShoppingCartApp.DTOs
{
    internal class ProductDTO
    {
        public ProductDTO()
        {
        }

        public string? ProductName { get; internal set; }
        public double ProductPrice { get; internal set; }
        public int ProductQuantity { get; internal set; }
        public string? ShoppingCartName { get; internal set; }
    }
}