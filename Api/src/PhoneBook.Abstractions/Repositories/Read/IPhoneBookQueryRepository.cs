using System;

namespace PhoneBook.Abstractions.Repositories.Read
{
    public interface IPhoneBookQueryRepository : IQueryRepository<Model.PhoneBook, Guid>
    {
    }
}
