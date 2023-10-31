namespace IntegrationEvents.Domain.Results
{
    public sealed record ShopNameWithId(string Id, string Name);

    public sealed record OrderShopQueryResult(IEnumerable<ShopNameWithId> ShopNameWithId)
    {
    }
}
