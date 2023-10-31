using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.Products;

public class ProductFactory
{
    private readonly CreateValues _createValues;
    private readonly string _image;
    private readonly Money _price;
    private readonly Category _category;

    public ProductFactory()
    {
        _createValues = new ("Test Product Name", "Test Product Description", true, 2);
        _image = "img.jpg";
        _price = new (10.9m, Currency.Pln, Unit.Kg);
        _category = Category.Meat;
        
        _createValues.AttachCategory(_category);
        _createValues.AttachImage(_image);
        _createValues.AttachPrice(_price);
    }

    public ProductFactory(CreateValues createValues, string image, Money price, Category category)
    {
        _createValues = createValues;
        _image = image;
        _price = price;
        _category = category;
        
        _createValues.AttachCategory(_category);
        _createValues.AttachImage(_image);
        _createValues.AttachPrice(_price);
    }

    public Product CreateProduct()
    {
        return Product.Create(_createValues);
    }
}