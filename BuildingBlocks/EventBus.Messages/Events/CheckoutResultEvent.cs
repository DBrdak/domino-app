using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class CheckoutResultEvent : IntegrationBaseEvent
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static CheckoutResultEvent Success(string orderId) => new CheckoutResultEvent
        {
            IsSuccess = true,
            Message = orderId
        };

        public static CheckoutResultEvent Failure(string message) => new CheckoutResultEvent
        {
            IsSuccess = false,
            Message = message
        };
    }
}