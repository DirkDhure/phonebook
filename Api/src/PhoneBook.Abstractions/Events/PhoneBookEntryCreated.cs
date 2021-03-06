﻿using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Model;
using System;

namespace PhoneBook.Abstractions.Events
{

    public class PhoneBookEntryCreated : IEvent<PhoneBookEntry>
    {
        public Guid EventId { get; }

        public DateTime EventTime { get; }

        public string Name => GetType().Name.ToLower();

        public PhoneBookEntryCreated(PhoneBookEntry phoneBookEntry)
        {
            this.EventId = Guid.NewGuid();
            this.EventTime = DateTime.Now;
            EventData = phoneBookEntry;
        }
        public PhoneBookEntry EventData { get; set; }
    }
}
