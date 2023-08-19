using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Abstractions
{
    public abstract class Entity : IEntity
    {
        public virtual string Id { get; init; }

        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(Guid id)
        {
            Id = id.ToString();
        }

        protected Entity()
        {
        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}