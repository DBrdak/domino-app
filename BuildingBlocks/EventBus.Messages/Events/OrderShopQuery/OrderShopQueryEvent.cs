namespace IntegrationEvents.Domain.Events.OrderShopQuery
{
    public sealed record OrderShopQueryEvent(IEnumerable<string> ShopsId)
    {
    }
}
