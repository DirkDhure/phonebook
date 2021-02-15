using PhoneBook.Abstractions.Messaging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PhoneBook.Abstractions.Model
{

    [DataContract]
    public class PhoneBookDetail : BaseQueryModel, ICommandData, IEventData
    {
        [DataMember(Name = "ownerEmail")]
        [Required]
        public string OwnerEmail { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

    }

}
