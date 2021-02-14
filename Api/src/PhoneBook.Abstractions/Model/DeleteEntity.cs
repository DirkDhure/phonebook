using PhoneBook.Abstractions.Messaging;
using System;


namespace PhoneBook.Abstractions.Model
{
    public class DeleteEntity : ICommandData,IEventData
    {
        public Guid EntityId { get; set; }
        public Guid? RootEntityId { get; set; }
    }
}
