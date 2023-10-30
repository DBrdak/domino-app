namespace Shared.Domain.Exceptions
{
    public class IntegrationEventException<TResult> : Exception
    {
        public Type ResultType = typeof(TResult);

        public IntegrationEventException(string message) : base(message)
        {
        }
    }
}
