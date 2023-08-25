using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Abstractions
{
    public abstract class Entity : IEntity
    {
        public string Id { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(string id)
        {
            Id = id;
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