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

        private List<IDomainEvent> _domainEvents = new();

        protected Entity(string id)
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            InitializeDomainEventsList();

            return _domainEvents;
        }

        public void ClearDomainEvents()
        {
            InitializeDomainEventsList();

            _domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            InitializeDomainEventsList();

            _domainEvents.Add(domainEvent);
        }

        private void InitializeDomainEventsList()
        {
            if (_domainEvents is null)
            {
                _domainEvents = new();
            }
        }
    }
}