namespace IntegrationEvents.Domain.Results
{
    public sealed record CheckoutOrderResult(string? OrderId, string? Error, bool IsSuccess)
    {
        public static CheckoutOrderResult Success(string orderId)
        {
            return new(orderId, null, true);
        }

        public static CheckoutOrderResult Failure(string message)
        {
            return new(null, message, false);
        }
    }
}