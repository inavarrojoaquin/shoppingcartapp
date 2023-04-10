using ShoppingCartApp.DTOs;

namespace ShoppingCartApp.App.Domain
{
    public class ProductName
    {
        private readonly string name;

        public ProductName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "ProductName"));

            this.name = name;
        }
        
        public static ProductName Create()
        {
            return new ProductName(Guid.NewGuid().ToString());
        }
        public string Value()
        {
            return name;
        }
    }
}