using IntegrationEvents.Domain.Events.OrderShopQuery;
using IntegrationEvents.Domain.Results;
using MassTransit;
using Shops.Domain.Abstractions;

namespace Shops.API.EventBusConsumers
{
    public class OrderShopQueryConsumer : IConsumer<OrderShopQueryEvent>
    {
        private readonly IShopRepository _shopRepository;

        public OrderShopQueryConsumer(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task Consume(ConsumeContext<OrderShopQueryEvent> context)
        {
            var shops = await _shopRepository.GetShops(context.Message.ShopsId, CancellationToken.None);

            await context.RespondAsync(new OrderShopQueryResult(shops.Select(s => new ShopNameWithId(s.Id, s.ShopName))));
        }
    }
}
