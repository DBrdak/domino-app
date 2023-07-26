using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ShoppingCart.API.Models;

public class PhoneNumber
{
    [RegularExpression("^[0-9]{9}$")]
    public string Number { get; }

    [RegularExpression("^\\+\\d{1,3}$")]
    public string Code { get; }

    public PhoneNumber(string number, string code)
    {
        Number = number;
        Code = code;
    }
}