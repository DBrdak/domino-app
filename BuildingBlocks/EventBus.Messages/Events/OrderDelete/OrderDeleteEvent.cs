namespace IntegrationEvents.Domain.Events.OrderDelete
{
    public sealed record OrderDeleteEvent(string ShopId, string OrderId)
    {
    }
}
