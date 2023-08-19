using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Abstractions
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> GetDomainEvents();

        void ClearDomainEvents();
    }
}