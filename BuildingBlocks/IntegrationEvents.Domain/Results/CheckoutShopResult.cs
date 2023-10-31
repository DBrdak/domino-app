namespace IntegrationEvents.Domain.Results
{
    public sealed record CheckoutShopResult(string ShopId, string? Error, bool IsSuccess)
    {
        public static CheckoutShopResult Success(string shopId)
        {
            return new(shopId, null, true);
        }

        public static CheckoutShopResult Failure(string message)
        {
            return new(null, message, false);
        }
    }
}
