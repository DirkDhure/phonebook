using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Model;
using System;

namespace PhoneBook.Abstractions.Commands
{

    public class DeletePhoneBook : ICommand<DeleteEntity>
    {
        public Guid CommandId { get; }

        public DeletePhoneBook()
        {
            CommandId = Guid.NewGuid();
        }
        public string UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public string UserEmail { get; set; }
        public DeleteEntity CommandData { get; set; }
        public string Name => GetType().Name.ToLower();
        public DateTime CommandTime { get; set; }
    }
}
