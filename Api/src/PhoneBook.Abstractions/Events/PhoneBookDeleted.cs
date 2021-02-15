using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Model;
using System;

namespace PhoneBook.Abstractions.Events
{

    public class PhoneBookDeleted : IEvent<DeleteEntity>
    {
        public PhoneBookDeleted(DeleteEntity deleteEntity)
        {
            this.EventId = Guid.NewGuid();
            this.EventTime = DateTime.UtcNow;
            this.EventData = deleteEntity;
        }
        public Guid EventId { get; }

        public DateTime EventTime { get; }
        public string Name => GetType().Name.ToLower();
        public DeleteEntity EventData { get; set; }

    }
}
