using System;

namespace PhoneBook.Abstractions.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool IsDeleted { get; set; }
        bool IsNew { get; set; }
    }
}
