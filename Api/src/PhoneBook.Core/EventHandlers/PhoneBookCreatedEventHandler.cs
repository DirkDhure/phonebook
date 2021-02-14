using Microsoft.Extensions.Logging;
using PhoneBook.Abstractions.Events;
using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Repositories.Read;
using System.Threading.Tasks;

namespace PhoneBook.Core.EventHandlers
{
        public class PhoneBookCreatedEventHandler : IEventHandler<PhoneBookCreated>
    {

        private IPhoneBookQueryRepository _readRepository;
        private ILogger<PhoneBookCreatedEventHandler> logger;
       
        public PhoneBookCreatedEventHandler(IPhoneBookQueryRepository readRepository, ILogger<PhoneBookCreatedEventHandler> logger)
        {
            _readRepository = readRepository;
            this.logger = logger;
        }

       public string HandlerName => GetType().Name.ToLower();

        public async Task HandleAsync(PhoneBookCreated @event)
        {
             logger.LogInformation("Loading phone book on the read db.............. ");
            var entity = await _readRepository.LoadModelAsync(@event.EventData.Id);

            if (entity == null)
            {
                logger.LogError("phone book not found");
            }
            logger.LogInformation("Saving phone book details.............");

            var phoneBook = new PhoneBook.Abstractions.Model.PhoneBook()
            {
                Id = @event.EventData.Id,
                BookDetail = @event.EventData,
            };
                       
            await _readRepository.SaveModelAsync(phoneBook);
            
        }
    }
}
