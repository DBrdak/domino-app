namespace OnlineShop.ShoppingCart.API.Exceptions
{
    public class WrongInputException : Exception
    {
        public WrongInputException(string message) : base(message)
        {
        }
    }
}