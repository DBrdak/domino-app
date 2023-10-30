namespace Shared.Domain.Exceptions
{
    public sealed class DomainException<T> : DomainException
    {
        public DomainException(string message) : base(message, typeof(T))
        {
            
        }
    }

    public class DomainException : Exception
    {
        public Type Type { get; init; }

        public DomainException(string message, Type type) : base(message)
        {
            Type = type;
        }
    }
}
