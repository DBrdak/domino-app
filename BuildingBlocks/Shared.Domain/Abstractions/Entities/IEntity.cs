namespace Shared.Domain.Abstractions.Entities
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> GetDomainEvents();

        void ClearDomainEvents();
    }
}