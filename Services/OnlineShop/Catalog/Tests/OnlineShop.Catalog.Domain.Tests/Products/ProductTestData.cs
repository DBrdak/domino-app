using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.Products;

public class ProductCreateSuccessTestData : TheoryData<CreateValues, Category, string, Money>
{
    public ProductCreateSuccessTestData()
    {
        Add(new ("Test1", "Desc1", true, 12), Category.Meat, "https://res.cloudinary.com/dsfafasdfdsaf", new (12, Currency.Pln, Unit.Kg));
        Add(new ("Test2", "Desc2", false, null), Category.Sausage, "https://res.cloudinary.com/dsfaf231asdfdsaf", new (12, Currency.Pln, Unit.Pcs));
    }
};
        
public class ProductCreateFailureTestData : TheoryData<CreateValues, Category, string, Money>
{
    public ProductCreateFailureTestData()
    {
        Add(new ("Test1", "Desc1", true, null), Category.Meat, "https://res.cloudinary.com/dsfafasdfdsaf", new (12, Currency.Pln, Unit.Kg));
        Add(new ("Test1", "Desc1", false, null), Category.Meat, String.Empty, new (12, Currency.Pln, Unit.Pcs));
        Add(new ("Test1", "Desc1", false, null), Category.Meat, String.Empty, new (12, Currency.Pln, Unit.Pcs));
        Add(new (String.Empty, String.Empty, false, null), Category.Meat, String.Empty, new (12, Currency.Pln, null));
    }
};

public class ProductPriceUpdateSuccessTestData : TheoryData<Money>
{
    public ProductPriceUpdateSuccessTestData()
    {
        Add(new (12.9m, Currency.Pln, Unit.Kg));
        Add(new (12.9m, Currency.Pln, Unit.Pcs));
    }
}

public class ProductPriceUpdateFailureTestData : TheoryData<Money>
{
    public ProductPriceUpdateFailureTestData()
    {
        Add(new (12.9m, Currency.Pln, null));
    }
}

public class ProductUpdateSuccessTestData : TheoryData<UpdateValues>
{
    public ProductUpdateSuccessTestData()
    {
        Add(new ("", "New Description 1", "https://res.cloudinary.com/dsfafasdfdsaf", true, 12.8m, false));
        Add(new ("", "New Description 2", "https://res.cloudinary.com/dsfaf12123saf", false, null, true));
    }
}

public class ProductUpdateFailureTestData : TheoryData<UpdateValues>
{
    public ProductUpdateFailureTestData()
    {
        Add(new ("", "New Description 1", "img3.jpg", false, 12.8m, false));
        Add(new ("", string.Empty, "img3.jpg", true, null, true));
        
    }
}