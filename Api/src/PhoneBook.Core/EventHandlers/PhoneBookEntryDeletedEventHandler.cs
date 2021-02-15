using Microsoft.Extensions.Logging;
using PhoneBook.Abstractions.Events;
using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Repositories.Read;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Core.EventHandlers
{

    public class PhoneBookEntryDeletedEventHandler : IEventHandler<PhoneBookEntryDeleted>
    {

        private IPhoneBookQueryRepository _readRepository;
        private ILogger<PhoneBookEntryDeletedEventHandler> _logger;

        public PhoneBookEntryDeletedEventHandler(IPhoneBookQueryRepository readRepository, ILogger<PhoneBookEntryDeletedEventHandler> logger)
        {
            _readRepository = readRepository;
            _logger = logger;
        }

        public string HandlerName => GetType().Name.ToLower();

        public async Task HandleAsync(PhoneBookEntryDeleted @event)
        {
           
            _logger.LogInformation("Loading phone book  on the query db.............");
            var phoneBook = await _readRepository.LoadModelAsync(@event.EventData.RootEntityId.Value);

            if (phoneBook != null)
            {
                var existingEntry = phoneBook.Entries.FirstOrDefault(e => e.Id == @event.EventData.EntityId);
                if (existingEntry != null)
                {
                    phoneBook.Entries.Remove(existingEntry);
                }
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
