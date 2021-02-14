using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Model;
using System;

namespace PhoneBook.Abstractions.Commands
{
    public class CreatePhoneBookEntry : ICommand<PhoneBookEntry>
    {
        public CreatePhoneBookEntry()
        {
            CommandId = Guid.NewGuid();
            CommandTime = DateTime.Now.ToLocalTime();
            Name = GetType().Name.ToLower();
        }

        public string UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string UserEmail { get; set; }
        public DateTime CommandTime { get; set; }
        public Guid CommandId { get; set; }
        public PhoneBookEntry CommandData { get; set; }
        public string Name { get; set; }
    }
}
