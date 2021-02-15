using EventStore.ClientAPI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PhoneBook.Abstractions;
using PhoneBook.Abstractions.Commands;
using PhoneBook.Abstractions.Enums;
using PhoneBook.Abstractions.Events;
using PhoneBook.Abstractions.Messaging;
using PhoneBook.Abstractions.Model;
using PhoneBook.Abstractions.Repositories.Read;
using PhoneBook.Abstractions.Repositories.Write;
using PhoneBook.Abstractions.Services;
using PhoneBook.Core.Aggregates;
using PhoneBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Core.Services
{
    public class PhoneBookApplication : IPhoneBookApplication
    {
        private readonly IPhoneBookRepository _phoneBookRepository;
        private readonly IPhoneBookQueryRepository _phoneBookQueryRepository;
        private readonly IOptions<ApplicationSettings> config;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILogger<PhoneBookApplication> _logger;

        public PhoneBookApplication(IPhoneBookRepository phoneBookRepository, IPhoneBookQueryRepository phoneBookQueryRepository, IEventDispatcher eventDispatcher, ILogger<PhoneBookApplication> logger)
        {
            _phoneBookQueryRepository = phoneBookQueryRepository;
            _phoneBookRepository = phoneBookRepository;
            _eventDispatcher = eventDispatcher;
            _logger = logger;
        }

        public async Task<CommandResult<Abstractions.Model.PhoneBook>> CreatePhoneBook(CreatePhoneBook command, RequestContext context)
        {
            // initialize aggregates

            var existing = (await _phoneBookRepository.FindAggregatesAsync(new List<SearchParameter>() {
                new SearchParameter
                {
                    Name="EMAILADDRESS",
                    Value= command.CommandData.OwnerEmail.ToLower()
                }
            }, FilterType.And)).FirstOrDefault();

            PhoneBookEntity entity;
            if (existing != null)
            {
                entity = (PhoneBookEntity)await _phoneBookRepository.LoadAggregateAsync(existing.Id);
            }
            else {
                entity = (PhoneBookEntity)await _phoneBookRepository.LoadAggregateAsync(Guid.Empty);
            }

             
            PhoneBookAggregate phoneBookAggregate = new PhoneBookAggregate(entity);
            CommandResult<Abstractions.Model.PhoneBook> commandResult;

            _logger.LogInformation("Creating phone book ......");
            var validationResult = phoneBookAggregate.CreatePhoneBook(command.CommandData);
            if (!validationResult.IsValid)
            {
                commandResult = new CommandResult<Abstractions.Model.PhoneBook>(Guid.Empty, null, false);
                foreach (var message in validationResult.ValidationMessages)
                {
                    commandResult.AddResultMessage(message.MessageType, message.Code, message.Message);
                }
                // return failed result
                return commandResult;
            }
            else
            {

                _logger.LogInformation("Saving phone book details ......");
                await _phoneBookRepository.SaveAggregateAsync(phoneBookAggregate.Entity);
               
                // publish aggregate events
                await PublishAggregateEvents(phoneBookAggregate);
                
                // save command
                await SaveCommand<CreatePhoneBook, PhoneBookDetail>(command);

            }
            var phoneBook = new Abstractions.Model.PhoneBook
            {
                BookDetail = phoneBookAggregate.Entity.BookDetail
            };

            commandResult = new CommandResult<Abstractions.Model.PhoneBook>(phoneBookAggregate.Id, phoneBook, true);

            // return result
            return commandResult;
        }

        public async Task<CommandResult<PhoneBookEntry>> CreatePhoneBookEntry(CreatePhoneBookEntry command, RequestContext context)
        {
            // initialize aggregates
            var entity = (PhoneBookEntity)await _phoneBookRepository.LoadAggregateAsync(command.CommandData.PhoneBookId);
            PhoneBookAggregate phoneBookAggregate = new PhoneBookAggregate(entity);
            CommandResult<PhoneBookEntry> commandResult;

            _logger.LogInformation("Loading phone book entry......");
            
            command.CommandData.Id = Guid.NewGuid();
            var validationResult = phoneBookAggregate.CreatePhoneBookEntry(command.CommandData);
            if (!validationResult.IsValid)
            {
                commandResult = new CommandResult<PhoneBookEntry>(Guid.Empty, null, false);
                foreach (var message in validationResult.ValidationMessages)
                {
                    commandResult.AddResultMessage(message.MessageType, message.Code, message.Message);
                }
                // return failed result
                return commandResult;
            }
            else
            {

                _logger.LogInformation("Saving phone book entry ......");
                await _phoneBookRepository.SaveAggregateAsync(phoneBookAggregate.Entity);

                // publish aggregate events
                await PublishAggregateEvents(phoneBookAggregate);

                // save command
                await SaveCommand<CreatePhoneBookEntry, PhoneBookEntry>(command);

            }
            var phoneBook = new Abstractions.Model.PhoneBook
            {
                BookDetail = phoneBookAggregate.Entity.BookDetail
            };

            commandResult = new CommandResult<PhoneBookEntry>(phoneBookAggregate.Id, command.CommandData, true);

            // return result
            return commandResult;
        }

        public async Task<CommandResult> DeletePhoneBook(DeletePhoneBook command, RequestContext context)
        {

            _logger.LogInformation("Loading phone book details for delete......");
            var entity = await _phoneBookRepository.LoadAggregateAsync(command.CommandData.EntityId);

            var aggregate = new PhoneBookAggregate(entity as PhoneBookEntity);
            var commandResult = new CommandResult(aggregate.Id, true);

            _logger.LogInformation("Deleting phone book......");
            aggregate.DeletePhoneBook();

            {
                
                await _phoneBookRepository.SaveAggregateAsync(aggregate.Entity);

                // publish aggregate events
                await PublishAggregateEvents(aggregate);

                // save command
                await SaveCommand<DeletePhoneBook, DeleteEntity>(command);
            }

            // return result
            return commandResult;
        }

        public async Task<CommandResult> DeletePhoneBookEntry(DeletePhoneBookEntry command, RequestContext context)
        {
            _logger.LogInformation("Loading phone book for deletng entry ......");
            var entity = await _phoneBookRepository.LoadAggregateAsync(command.CommandData.RootEntityId.Value);

            if (entity.Entries != null && entity.Entries.Any()) {

                var entry = entity.Entries.FirstOrDefault(e => e.Id == command.CommandData.EntityId);
                if (entry != null) {

                    entry.IsDeleted = true;
                }
            
            }

            var aggregate = new PhoneBookAggregate(entity as PhoneBookEntity);
             
            var commandResult = new CommandResult(aggregate.Id, true);

            _logger.LogInformation("Deleting phone book entry......");
            aggregate.DeletePhoneBook();

            {

                await _phoneBookRepository.SaveAggregateAsync(aggregate.Entity);

                // publish aggregate events
                await PublishAggregateEvents(aggregate);

                // save command
                await SaveCommand<DeletePhoneBookEntry, DeleteEntity>(command);
            }

            // return result
            return commandResult;
        }

        public async Task<CommandResult<PhoneBookEntry>> UpdatePhoneBookEntry(UpdatePhoneBookEntry command, RequestContext context)
        {
            // initialize aggregates
            var entity = (PhoneBookEntity)await _phoneBookRepository.LoadAggregateAsync(command.CommandData.PhoneBookId);
            PhoneBookAggregate phoneBookAggregate = new PhoneBookAggregate(entity);
            CommandResult<PhoneBookEntry> commandResult;

            _logger.LogInformation("Loading phone book entry......");

            var validationResult = phoneBookAggregate.UpdatePhoneBookEntry(command.CommandData);
            if (!validationResult.IsValid)
            {
                commandResult = new CommandResult<PhoneBookEntry>(Guid.Empty, null, false);
                foreach (var message in validationResult.ValidationMessages)
                {
                    commandResult.AddResultMessage(message.MessageType, message.Code, message.Message);
                }
                // return failed result
                return commandResult;
            }
            else
            {

                _logger.LogInformation("Saving phone book entry ......");
                await _phoneBookRepository.SaveAggregateAsync(phoneBookAggregate.Entity);

                // publish aggregate events
                await PublishAggregateEvents(phoneBookAggregate);

                // save command
                await SaveCommand<UpdatePhoneBookEntry, PhoneBookEntry>(command);

            }
            var phoneBook = new Abstractions.Model.PhoneBook
            {
                BookDetail = phoneBookAggregate.Entity.BookDetail
            };

            commandResult = new CommandResult<PhoneBookEntry>(phoneBookAggregate.Id, command.CommandData, true);

            // return result
            return commandResult;
        }

        public async Task<CommandResult<PhoneBookEntry>> UpdatePhoneBookEntryAvatar(UpdatePhoneBookEntry command, RequestContext context)
        {
            throw new NotImplementedException();
        }

        private async Task PublishAggregateEvents<T>(IAggregate<T> aggregate) where T : IAggregateRoot
        {

            if (aggregate.Events != null && aggregate.Events.Any())
            {
                await _eventDispatcher.DispatchAsync(aggregate.Events.ToArray());
                aggregate.ClearEvents();
            }
        }

        private async Task SaveCommand<T, TI>(T command) where T : ICommand<TI> where TI : ICommandData
        {

            return;
            /// use this when eventstore is configured correctly
            using (var _eventStoreConnection = EventStoreConnection.Create(new Uri(config.Value.EventStoreConnectionString), config.Value.EventStoreStreamName))
            {
                //eventData for  event store
                var json = JsonConvert.SerializeObject(command);
                var data = Encoding.UTF8.GetBytes(json);
                var CommandName = command.Name;
                var CommandData = new EventData(Guid.NewGuid(), CommandName, true, data, new byte[] { });

                //EventStore Connection
                await _eventStoreConnection.ConnectAsync();
                await _eventStoreConnection.AppendToStreamAsync("PhoneBookManager-stream", ExpectedVersion.Any, CommandData);
            }

        }
    }
}
