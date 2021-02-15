using PhoneBook.Abstractions.Model;
using PhoneBook.Abstractions.Repositories.Read;
using PhoneBook.Abstractions.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Core.Services
{
    public class PhoneBookQueryHandler: IPhoneBookQueryHandler
    {
        private IPhoneBookQueryRepository _phoneBookRepository;


        public PhoneBookQueryHandler(IPhoneBookQueryRepository phoneBookRepository) {

            _phoneBookRepository = phoneBookRepository;
        }
        public async Task<Abstractions.Model.PhoneBook> GetPhoneBook(string emailAddress, RequestContext context)
        {

            List<SearchParameter> searchParameters = new List<SearchParameter>
            {
                new SearchParameter
                {
                    Name = "EMAILADDRESS",
                    Value = emailAddress.ToLower()
                }
            };


            var books = await _phoneBookRepository.FindModelsAsync(searchParameters);

            return books.FirstOrDefault();

        }
    }
}
