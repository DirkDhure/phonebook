using System.Collections.Generic;
using PhoneBook.Abstractions.Messaging;

namespace PhoneBook.Abstractions.Model
{
    public interface IAggregate<T> where T : IAggregateRoot
    {
        T Entity { get; }
        IEnumerable<IEvent> Events { get; }
        void AddEvent(IEvent domainEvent);
        void ClearEvents();
    }
}
