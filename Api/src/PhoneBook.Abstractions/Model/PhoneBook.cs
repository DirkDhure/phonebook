using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhoneBook.Abstractions.Model
{

    [DataContract]
    public class PhoneBook : BaseQueryModel
    {
        [DataMember(Name = "bookDetail")]
        public PhoneBookDetail BookDetail { get; set; }

        [DataMember(Name = "entries")]
        public List<PhoneBookEntry> Entries { get; set; }

    }
}
