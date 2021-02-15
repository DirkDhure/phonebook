using PhoneBook.Core.Entities;
using System;
using System.Collections.Generic;

namespace PhoneBook.Core.Services
{

    public class EntityFactory
    {
        public static PhoneBookEntity CreatePhoneBookEntity()
        {
            return new PhoneBookEntity()
            {
                Id = Guid.NewGuid(),
                Entries= new List<Abstractions.Model.PhoneBookEntry>(),                
               IsNew= true
            };
        }
    }

}
