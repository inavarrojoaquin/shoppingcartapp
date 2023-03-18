namespace ShoppingCartApp
{
    internal class Discount
    {
        private readonly Dictionary<string, double> promotions;
        public double AppliedDiscount { get; private set; }
        public string AppliedPromotion { get; private set; }
        public Discount()
        {
            AppliedDiscount = 0;
            promotions = new Dictionary<string, double>
            {
                { "PROMO_5", 5},
                { "PROMO_10", 10},
                { "PROMO_15", 15},
            };
        }
        internal void ApplyPromotion(string promotion)
        {
            if (!promotions.ContainsKey(promotion))
                throw new Exception("Promotion does not exists");

            AppliedDiscount = promotions[promotion];
            AppliedPromotion = promotion;
        }
    }
}