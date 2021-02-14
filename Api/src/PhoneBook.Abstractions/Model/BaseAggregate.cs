using System;
using System.Collections.Generic;
using PhoneBook.Abstractions.Messaging;

namespace PhoneBook.Abstractions.Model
{
    public abstract class BaseAggregate<T> : IAggregate<T> where T : IAggregateRoot
    {
        private readonly IDictionary<Type, IEvent> _events = new Dictionary<Type, IEvent>();

        protected T entity ;
        
        public BaseAggregate(T entity) {
            this.entity = entity;
        }

        public T Entity => this.entity;

        public Guid Id => this.entity.Id;

        public IEnumerable<IEvent> Events => _events.Values;

       
        public void AddEvent(IEvent domainEvent)
        {
            _events[domainEvent.GetType()] = domainEvent;
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
