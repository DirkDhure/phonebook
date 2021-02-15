using Microsoft.Extensions.Logging;
using PhoneBook.Abstractions.Events;
using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Repositories.Read;
using System.Threading.Tasks;

namespace PhoneBook.Core.EventHandlers
{

    public class PhoneBookDeletedEventHandler : IEventHandler<PhoneBookDeleted>
    {

        private IPhoneBookQueryRepository _readRepository;
        private ILogger<PhoneBookDeletedEventHandler> _logger;

        public PhoneBookDeletedEventHandler(IPhoneBookQueryRepository readRepository, ILogger<PhoneBookDeletedEventHandler> logger)
        {
            _readRepository = readRepository;
            _logger = logger;
        }

        public string HandlerName => GetType().Name.ToLower();

        public async Task HandleAsync(PhoneBookDeleted @event)
        {

            _logger.LogInformation("Loading phone book  on the query db.............");
            var phoneBook = await _readRepository.LoadModelAsync(@event.EventData.RootEntityId.Value);

            if (phoneBook != null)
            {
                phoneBook.IsDeleted = true;
            }
            else
            {
                _logger.LogError("Phone book not found");
            }
            _logger.LogInformation("Saving phone book..................");
            await _readRepository.SaveModelAsync(phoneBook);
        }
    }
}
