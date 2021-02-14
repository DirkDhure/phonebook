using System;

namespace PhoneBook.Abstractions.Model
{
    public interface IAggregateRoot : IEntity
    {
        DateTime LastProcessedEventTime { get; }
    }
}
