using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;

namespace OnlineShop.ShoppingCart.API.EventBusConsumer
{
    public class CheckoutResultConsumer : IConsumer<CheckoutResultEvent>
    {
        private static CheckoutResultEvent _result = null;

        public static async Task<CheckoutResultEvent> GetCheckoutResult()
        {
            while (_result is null)
            {
                await Task.Delay(5);
            }

            return _result;
        }

        public async Task Consume(ConsumeContext<CheckoutResultEvent> context)
        {
            _result = context.Message;
            //TODO SMS here
        }
    }
}