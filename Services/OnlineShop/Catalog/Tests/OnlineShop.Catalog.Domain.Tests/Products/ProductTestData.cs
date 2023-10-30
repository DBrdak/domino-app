using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.Products;

public class ProductSuccessTestData : TheoryData<CreateValues, Category, string, Money>
{
    public ProductSuccessTestData()
    {
        Add(new ("Test1", "Desc1", true, 12), Category.Meat, "img1.jpg", new (12, Currency.Pln, Unit.Kg));
        Add(new ("Test2", "Desc2", false, null), Category.Sausage, "img2.jpg", new (12, Currency.Pln, Unit.Pcs));
    }
};
        
public class ProductFailureTestData : TheoryData<CreateValues, Category, string, Money>
{
    public ProductFailureTestData()
    {
        Add(new ("Test1", "Desc1", true, null), Category.Meat, "img1.jpg", new (12, Currency.Pln, Unit.Kg));
        Add(new ("Test1", "Desc1", false, null), Category.Meat, String.Empty, new (12, Currency.Pln, Unit.Pcs));
        Add(new ("Test1", "Desc1", false, null), Category.Meat, String.Empty, new (12, Currency.Pln, Unit.Pcs));
        Add(new (String.Empty, String.Empty, false, null), Category.Meat, String.Empty, new (12, Currency.Pln, null));
    }
};