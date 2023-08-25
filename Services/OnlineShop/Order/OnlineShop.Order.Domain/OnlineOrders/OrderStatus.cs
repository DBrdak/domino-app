using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Order.Domain.OrderItems
{
    public sealed record OrderStatus
    {
        public string StatusMessage { get; set; }
        private readonly int _code;

        private OrderStatus(int code, string status)
        {
            _code = code;
            StatusMessage = status;
        }

        public static readonly OrderStatus Validating = new(1, "Potwierdź kod SMS");
        public static readonly OrderStatus Waiting = new(2, "Oczekuje na potwierdzenie");
        public static readonly OrderStatus Accepted = new(3, "Potwierdzone");
        public static readonly OrderStatus Modified = new(4, "Potwierdzone ze zmianami");
        public static readonly OrderStatus Cancelled = new(5, "Anulowane");
        public static readonly OrderStatus Rejected = new(6, "Odrzucone");
        public static readonly OrderStatus Received = new(7, "Odebrane");

        public static readonly IReadOnlyCollection<OrderStatus> All = new[]
        {
            Validating, Waiting, Accepted, Modified, Cancelled, Rejected
        };

        public static OrderStatus FromMessage(string message)
            => All.FirstOrDefault(m => m.StatusMessage == message) ??
               throw new ApplicationException("Invalid status message");

        public static OrderStatus FromCode(int code)
            => All.FirstOrDefault(m => m._code == code) ??
               throw new ApplicationException("Invalid status message");
    }
}