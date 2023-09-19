namespace EventBus.Domain.Common
{
    public sealed record CheckoutResult(string? OrderId, string? Error, bool IsSuccess)
    {
        public static CheckoutResult Success(string orderId)
        {
            return new(orderId, null, true);
        }

        public static CheckoutResult Failure(string message)
        {
            return new(null, message, false);
        }
    }
}