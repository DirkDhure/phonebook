using PhoneBook.Abstractions.Model;
using PhoneBook.Abstractions.Model.Entities;
using System;
using System.Collections.Generic;

namespace PhoneBook.Core.Entities
{
    public class PhoneBookEntity : IPhoneBookEntity
    {
        public PhoneBookDetail BookDetail { get; set; }
        public List<PhoneBookEntry> Entries { get; set; }

        public DateTime LastProcessedEventTime { get; set; }

        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
    }
}
