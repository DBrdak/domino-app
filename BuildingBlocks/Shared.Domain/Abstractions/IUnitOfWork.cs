using Shared.Domain.Abstractions.Entities;

namespace Shared.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(List<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}