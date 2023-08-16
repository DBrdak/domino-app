namespace EventBus.Messages.Events
{
    public class CheckoutResultResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static CheckoutResultResponse Success(string orderId) => new CheckoutResultResponse
        {
            IsSuccess = true,
            Message = orderId
        };

        public static CheckoutResultResponse Failure(string message) => new CheckoutResultResponse
        {
            IsSuccess = false,
            Message = message
        };
    }
}