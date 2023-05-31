namespace ShoppingCartApp.App.Modules.ProductModule.Domain
{
    public class ProductId
    {
        private readonly Guid guid;

        public ProductId(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception(string.Format("Error: {0} can not be null or empty", "ProductId"));

            this.guid = Guid.ParseExact(id, "D");
        }

        public static ProductId Create()
        {
            return new ProductId(Guid.NewGuid().ToString());   
        }

        public string Value()
        {
            return this.guid.ToString();
        }
        
    }
}
