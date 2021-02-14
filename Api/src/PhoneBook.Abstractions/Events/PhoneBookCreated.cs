using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Abstractions.Events
{
   
    public class PhoneBookCreated : IEvent<PhoneBookDetail>
    {
        public Guid EventId { get; }

        public DateTime EventTime { get; }

        public string Name => GetType().Name.ToLower();

        public PhoneBookCreated(PhoneBookDetail phoneBookDetail)
        {
            this.EventId = Guid.NewGuid();
            this.EventTime = DateTime.Now;
            EventData = phoneBookDetail;
        }
        public PhoneBookDetail EventData { get; set; }
    }
}
