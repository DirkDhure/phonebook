using PhoneBook.Abstractions.Model.Entities;
using System;

namespace PhoneBook.Abstractions.Repositories.Write
{
    public interface IPhoneBookRepository : IRepository<IPhoneBookEntity, Guid>
    {
    }
    
}
