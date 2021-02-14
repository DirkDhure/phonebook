using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PhoneBook.Abstractions;
using PhoneBook.Abstractions.Enums;
using PhoneBook.Abstractions.Model;
using PhoneBook.Abstractions.Model.Entities;
using PhoneBook.Abstractions.Repositories.Write;
using PhoneBook.Core.Entities;
using PhoneBook.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PhoneBook.Infrastructure.Data.MongoDB.Write
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        private readonly MongoContext _context;

        public PhoneBookRepository()
        {
            _context = new MongoContext();
        }

        public PhoneBookRepository(IOptions<ApplicationSettings> config)
        {
            _context = new MongoContext(config);
        }
        public PhoneBookRepository(string servername, string databaseName)
        {
            _context = new MongoContext(servername, databaseName);
        }

        public async Task<IEnumerable<IPhoneBookEntity>> FindAggregatesAsync(List<SearchParameter> searchParameters, FilterType filterType)
        {
            FilterDefinition<PhoneBookEntity> filter = Builders<PhoneBookEntity>.Filter.Ne("isDeleted", true);

            foreach (var parameter in searchParameters.Where(parameter => !string.IsNullOrEmpty(parameter.Name) && !string.IsNullOrEmpty(parameter.Value)))

            {
                var validParameter = Enum.TryParse(parameter.Name.ToUpper(), out SearchOptions option);
                if (!validParameter)
                {
                    continue;
                }

                switch (option)
                {

                    case SearchOptions.PHONEBOOKID:
                        if (filter == null)
                        {
                            filter = Builders<PhoneBookEntity>.Filter.Eq("_id", Guid.Parse(parameter.Value));
                        }
                        else
                        {
                            if (filterType == FilterType.And)
                            {
                                filter = Builders<PhoneBookEntity>.Filter.Eq("_id", Guid.Parse(parameter.Value)) & filter;
                            }
                            if (filterType == FilterType.Or)
                            {
                                filter = Builders<PhoneBookEntity>.Filter.Eq("_id", Guid.Parse(parameter.Value)) | filter;
                            }
                        }
                        break;
                   
                    
                }

            }
            if (filter == null) throw new ArgumentException("Invalid search parameters specified");
            List<PhoneBookEntity> result = await _context.PhoneBooks.Find(filter).ToListAsync();
            return result;
        }

        public async Task<IPhoneBookEntity> LoadAggregateAsync(Guid id)
        {
            FilterDefinition<PhoneBookEntity> filter = Builders<PhoneBookEntity>.Filter.Ne("isDeleted", true);

             filter = Builders<PhoneBookEntity>.Filter.Eq("_id", id) & filter;
            if (filter == null)
            {
                throw new ArgumentException("Invalid search parameters specified");
            }

            var result = (await _context.PhoneBooks.FindAsync(filter)).FirstOrDefault();
            return result?? EntityFactory.CreatePhoneBookEntity();
        }

        public async Task<Guid> SaveAggregateAsync(IPhoneBookEntity aggregate)
        {
            FilterDefinition<PhoneBookEntity> filter = Builders<PhoneBookEntity>.Filter.Eq("_id", aggregate.Id);

            var result = await _context.PhoneBooks.FindAsync(filter);
            if (result.Any())
            {
                await _context.PhoneBooks.ReplaceOneAsync(filter, aggregate as PhoneBookEntity);
            }
            else
            {
                await _context.PhoneBooks.InsertOneAsync(aggregate as PhoneBookEntity);

            }

            return aggregate.Id;
        }

       
    }
}
