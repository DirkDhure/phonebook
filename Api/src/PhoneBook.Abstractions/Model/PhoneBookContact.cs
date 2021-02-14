using PhoneBook.Abstractions.Enums;
using PhoneBook.Abstractions.Messaging;
using System.Runtime.Serialization;

namespace PhoneBook.Abstractions.Model
{
    [DataContract]
    public class PhoneBookContact : BaseQueryModel, ICommandData, IEventData
    {
        [DataMember(Name = "contactType")]
        public ContactType ContactType { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

    }
   
}
