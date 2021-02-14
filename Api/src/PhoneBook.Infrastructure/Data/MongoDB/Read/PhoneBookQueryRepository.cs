using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PhoneBook.Abstractions;
using PhoneBook.Abstractions.Enums;
using PhoneBook.Abstractions.Model;
using PhoneBook.Abstractions.Repositories.Read;
using PhoneBook.Infrastructure.Data.MongoDB.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PhoneBook.Infrastructure.Data.MongoDB.Read
{
    public class PhoneBookQueryRepository : IPhoneBookQueryRepository

    {
        private readonly MongoContext _context;


        public PhoneBookQueryRepository(IOptions<ApplicationSettings> config)
        {
            _context = new MongoContext(config);
        }

        public async Task<IEnumerable<PhoneBook.Abstractions.Model.PhoneBook>> FindModelAsync(List<SearchParameter> searchParameters)
        {
            FilterDefinition<PhoneBook.Abstractions.Model.PhoneBook> filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Ne("isDeleted", true);

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
                            filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Eq("_id", Guid.Parse(parameter.Value));
                        }
                        else
                        {
                            
                                filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Eq("_id", Guid.Parse(parameter.Value)) & filter;
                            
                            
                        }
                        break;

                    case SearchOptions.EMAILADDRESS:
                        if (filter == null)
                        {
                            filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Eq("phoneBookDetails.emailAddress", parameter.Value.ToLower());
                        }
                        else
                        {

                            filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Eq("phoneBookDetails.emailAddress", parameter.Value.ToLower()) & filter;


                        }
                        break;
                }

            }
            if (filter == null) throw new ArgumentException("Invalid phone search parameters specified");
            List<PhoneBook.Abstractions.Model.PhoneBook> result = await _context.PhoneBooks.Find(filter).ToListAsync();
            return result;
        }
              
        public async Task<Guid> SaveModelAsync(PhoneBook.Abstractions.Model.PhoneBook model)
        {
            FilterDefinition<PhoneBook.Abstractions.Model.PhoneBook> filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Eq("_id", model.Id);

            IAsyncCursor<PhoneBook.Abstractions.Model.PhoneBook> result = await _context.PhoneBooks.FindAsync(filter);
            if (result.Any())
            {
                await _context.PhoneBooks.ReplaceOneAsync(filter, model);
            }
            else
            {
                await _context.PhoneBooks.InsertOneAsync(model);

            }

            return model.Id;
        }

        public async Task<IEnumerable<PhoneBook.Abstractions.Model.PhoneBook>> FindModelsAsync(List<SearchParameter> searchParameters)
        {
            FilterDefinition<PhoneBook.Abstractions.Model.PhoneBook> filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Ne("isDeleted", true);

            foreach (var parameter in searchParameters.Where(
                             parameter => !string.IsNullOrEmpty(parameter.Name) && !string.IsNullOrEmpty(parameter.Value)))
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
                            filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Eq("_id", Guid.Parse(parameter.Value));
                        }
                        else
                        {
                            filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Eq("_id", Guid.Parse(parameter.Value)) & filter;

                        }
                        break;
                }

            }
            if (filter == null) throw new ArgumentException("Invalid  search parameters specified");
            List<PhoneBook.Abstractions.Model.PhoneBook> result = await _context.PhoneBooks.Find(filter).ToListAsync();
            return result;
        }

        public async Task<PhoneBook.Abstractions.Model.PhoneBook> LoadModelAsync(Guid modelId)
        {
            var filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Eq("_id", modelId);
            filter = Builders<PhoneBook.Abstractions.Model.PhoneBook>.Filter.Ne("isDeleted", true) & filter;
            if (filter == null)
            {
                throw new ArgumentException("Invalid entity program search parameters specified");
            }

            IAsyncCursor<PhoneBook.Abstractions.Model.PhoneBook> result = await _context.PhoneBooks.FindAsync(filter);
            return result.FirstOrDefault();
        }
    }

}
