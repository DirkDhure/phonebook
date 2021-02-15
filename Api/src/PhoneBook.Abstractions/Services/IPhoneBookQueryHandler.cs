using PhoneBook.Abstractions.Model;
using System.Threading.Tasks;

namespace PhoneBook.Abstractions.Services
{

    public interface IPhoneBookQueryHandler
    {
        Task<Model.PhoneBook> GetPhoneBook(string emailAddress, RequestContext context);
      
    }
}
