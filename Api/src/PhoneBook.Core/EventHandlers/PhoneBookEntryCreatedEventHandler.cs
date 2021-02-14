using Microsoft.Extensions.Logging;
using PhoneBook.Abstractions.Events;
using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Model;
using PhoneBook.Abstractions.Repositories.Read;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Core.EventHandlers
{


    public class PhoneBookEntryCreatedEventHandler : IEventHandler<PhoneBookEntryCreated>
    {
        private IPhoneBookQueryRepository _readRepository;
        private ILogger<PhoneBookEntryCreated> logger;

        public PhoneBookEntryCreatedEventHandler(IPhoneBookQueryRepository readRepository, ILogger<PhoneBookEntryCreated> logger)
        {
            _readRepository = readRepository;
            this.logger = logger;
        }

        public string HandlerName => GetType().Name.ToLower();

        public async Task HandleAsync(PhoneBookEntryCreated @event)
        {
            logger.LogInformation("Loading phone book on the read db.............. ");
            var phoneBook = await _readRepository.LoadModelAsync(@event.EventData.PhoneBookId);
            var entry = @event.EventData;
            if (phoneBook != null)
            {
                entry.PhoneBookId = phoneBook.Id;
                if (phoneBook.Entries == null)
                {
                    phoneBook.Entries = new List<PhoneBookEntry>();
                }
                var existingEntry = phoneBook.Entries.FirstOrDefault(e => e.Id == entry.Id);
                if (existingEntry != null)
                {
                    phoneBook.Entries.Remove(existingEntry);
                }
                phoneBook.Entries.Add(entry);
            }
            logger.LogInformation("Saving phone book entry .............");
            await _readRepository.SaveModelAsync(phoneBook);

        }
    }
}
