using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Model;
using System;

namespace PhoneBook.Abstractions.Events
{
    public class PhoneBookNameUpdated : IEvent<PhoneBookDetail>
    {
        public Guid EventId { get; }

        public DateTime EventTime { get; }

        public string Name => GetType().Name.ToLower();

        public PhoneBookNameUpdated(PhoneBookDetail phoneBookEntry)
        {
            this.EventId = Guid.NewGuid();
            this.EventTime = DateTime.Now;
            EventData = phoneBookEntry;
        }
        public PhoneBookDetail EventData { get; set; }
    }
}
