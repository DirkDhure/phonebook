using PhoneBook.Abstractions.Commands;
using PhoneBook.Abstractions.Model;
using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Services
{
    public interface IPhoneBookApplication
    {
        Task<CommandResult<Model.PhoneBook>> CreatePhoneBook(CreatePhoneBook command, RequestContext context);
        Task<CommandResult<PhoneBookDetail>> CreatePhoneBookEntry(CreatePhoneBookEntry command, RequestContext context);
        Task<CommandResult<PhoneBookEntry>> UpdatePhoneBookEntry(UpdatePhoneBookEntry command, RequestContext context);
        Task<CommandResult<PhoneBookEntry>> UpdatePhoneBookEntryAvatar(UpdatePhoneBookEntry command, RequestContext context);
        Task<CommandResult> DeletePhoneBook(DeletePhoneBook command, RequestContext context);
        Task<CommandResult> DeletePhoneBookEntry(DeletePhoneBookEntry command, RequestContext context);
    }
}
