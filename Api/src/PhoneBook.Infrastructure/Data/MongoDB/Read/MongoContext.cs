
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using PhoneBook.Abstractions;

namespace PhoneBook.Infrastructure.Data.MongoDB.Read
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }
        private readonly string _serverName;
        private readonly string _databaseName ;
        private readonly ConventionPack camelConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        private readonly ConventionPack ignoreExtraElements = new ConventionPack { new IgnoreExtraElementsConvention(true) };
        private readonly ConventionPack ignoreNullsPack = new ConventionPack { new IgnoreIfNullConvention(true) };
        private readonly MongoClient client;
        public string ServerName => _serverName;
        public string DatabaseName => _databaseName;

        public MongoContext()
        {
            client = new MongoClient(_serverName);
            Database = client.GetDatabase(_databaseName);
        }
        public MongoContext(IOptions<ApplicationSettings> config)
        {
            _serverName = config.Value.QueryServerName;
            _databaseName = config.Value.QueryDatabaseName;

            ConventionPack pack = new ConventionPack
            {
                new IgnoreIfNullConvention(true),
                  new IgnoreExtraElementsConvention(true),
                new CamelCaseElementNameConvention()
            };
            ConventionRegistry.Register("defaults", pack, t => true);
            client = new MongoClient(_serverName);
            Database = client.GetDatabase(_databaseName);
        }
        public MongoContext(string serverName, string databaseName)
        {
            _serverName = serverName;
            _databaseName = databaseName;
            MongoClient client = new MongoClient(_serverName);
            ConventionRegistry.Register("CamelCaseConvensions", camelConventionPack, t => true);
            ConventionRegistry.Register("Ignore extra elements", ignoreExtraElements, t => true);
            ConventionRegistry.Register("Ignore null values", ignoreNullsPack, t => true);
            Database = client.GetDatabase(_databaseName);
        }

        
        public IMongoCollection<Abstractions.Model.PhoneBook> PhoneBooks => Database.GetCollection<Abstractions.Model.PhoneBook>("PhoneBooks");
        }
}
