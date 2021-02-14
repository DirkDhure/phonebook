using System.Collections.Generic;

namespace PhoneBook.Abstractions.Model.Entities
{
    public interface IPhoneBookEntity : IAggregateRoot
    {
        PhoneBookDetail BookDetail { get; set; }
        List<PhoneBookEntry> Entries { get; set; }
      
    }
}
