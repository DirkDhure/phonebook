using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Abstractions.Events
{
    public class PhoneBookEntryUpdated : IEvent<PhoneBookEntry>
    {
        public Guid EventId { get; }

        public DateTime EventTime { get; }

        public string Name => GetType().Name.ToLower();

        public PhoneBookEntryUpdated(PhoneBookEntry phoneBookEntry)
        {
            this.EventId = Guid.NewGuid();
            this.EventTime = DateTime.Now;
            EventData = phoneBookEntry;
        }
        public PhoneBookEntry EventData { get; set; }
    }
}
