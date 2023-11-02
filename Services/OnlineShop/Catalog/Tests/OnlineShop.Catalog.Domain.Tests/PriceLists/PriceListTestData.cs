using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.PriceLists;


public static class TestPriceLists
{
    public static PriceList RetailPriceList = PriceList.CreateRetail("Retail Test", Category.Meat);
    public static PriceList BusinessPriceList = PriceList.CreateBusiness("Business Test", "Contractor Test", Category.Meat);
}

public class AddLineItemTestData : TheoryData<string, Money>
{
    public AddLineItemTestData()
    {
        Add("Test Line Item 1", new (15.3m, Currency.Pln, Unit.Kg));
    }
}

public class CreateLineItemSuccessTestData : TheoryData<string, Money>
{
    public CreateLineItemSuccessTestData()
    {
        Add("Test Line Item 1", new (15.3m, Currency.Pln, Unit.Kg));
    }
}

public class CreateLineItemFailureTestData : TheoryData<string, Money>
{
    public CreateLineItemFailureTestData()
    {
        Add("Test Line Item 1", new (15.3m, Currency.Pln));
        Add(" ", new (15.3m, Currency.Pln, Unit.Kg));
    }
}

public class UpdateLineItemSuccessTestData : TheoryData<string, Money>
{
    public UpdateLineItemSuccessTestData()
    {
        Add("Test Line Item", new Money(12.5m, Currency.Pln, Unit.Kg));
    }
}

public class UpdateLineItemFailureTestData : TheoryData<string, Money>
{
    public UpdateLineItemFailureTestData()
    {
        Add("", new Money(12.5m, Currency.Pln, Unit.Kg));
    }
}