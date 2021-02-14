using PhoneBook.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhoneBook.Abstractions.Model
{

    [DataContract]
    public class PhoneBookEntry : BaseQueryModel, ICommandData, IEventData
    {
        [DataMember(Name = "phoneBookId")]
        public Guid PhoneBookId { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "companyName")]
        public string CompanyName { get; set; }

        [DataMember(Name = "contacts")]
        public List<PhoneBookContact> Contacts { get; set; }

    }
}
