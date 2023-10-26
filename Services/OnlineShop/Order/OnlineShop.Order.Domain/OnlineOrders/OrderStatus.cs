namespace OnlineShop.Order.Domain.OnlineOrders
{
    public sealed record OrderStatus
    {
        public string StatusMessage { get; set; }

        private OrderStatus()
        { }

        private OrderStatus(string status)
        {
            StatusMessage = status;
        }

        public static readonly OrderStatus Validating = new("Potwierdź kod SMS");
        public static readonly OrderStatus Waiting = new("Oczekuje na potwierdzenie");
        public static readonly OrderStatus Accepted = new("Potwierdzone");
        public static readonly OrderStatus Modified = new("Potwierdzone ze zmianami");
        public static readonly OrderStatus Cancelled = new("Anulowane");
        public static readonly OrderStatus Rejected = new("Odrzucone");
        public static readonly OrderStatus Received = new("Odebrane");

        public static readonly IReadOnlyCollection<OrderStatus> All = new[]
        {
            Validating, Waiting, Accepted, Modified, Cancelled, Rejected
        };

        public static OrderStatus FromMessage(string message)
            => All.FirstOrDefault(m => m.StatusMessage == message) ??
               throw new ApplicationException("Invalid status message");
    }
}