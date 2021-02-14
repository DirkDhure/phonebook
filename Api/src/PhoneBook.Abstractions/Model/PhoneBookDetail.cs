using PhoneBook.Abstractions.Messaging;
using System;
using System.Runtime.Serialization;

namespace PhoneBook.Abstractions.Model
{

    [DataContract]
    public class PhoneBookDetail : BaseQueryModel, ICommandData, IEventData
    {
        [DataMember(Name = "ownerEmail")]
        public string OwnerEmail { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "DateCreated")]
        public DateTime DateCreated { get; set; }

    }

}
