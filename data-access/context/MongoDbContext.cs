using data_access.Settings;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using data_access.entities.MongoDBEntities;
using data_access.Settings;

namespace data_access.context
{
    public class MongoDbContext
    {
        public MongoClient MongoClient { get; }
        public IMongoDatabase MongoDatabase { get; }

        public MongoDbContext(IConfiguration configuration)
        {
            //Get connection setting
            var mongoDBSettings = configuration.GetSection("MongoDB").Get<MongoDbSettings>();

            MongoClientSettings settings = MongoClientSettings.FromConnectionString(mongoDBSettings.ConnectionURI);
            settings.LinqProvider = LinqProvider.V3;
            MongoClient = new MongoClient(settings);
            MongoDatabase = MongoClient.GetDatabase(mongoDBSettings.DatabaseName);


            Configurations = MongoDatabase.GetCollection<ConfigurationEntity>();
            BuildingTypes = MongoDatabase.GetCollection<BuildingTypeEntity>();
        }

        public IMongoCollection<ConfigurationEntity> Configurations { get; set; } = null!;
        public IMongoCollection<BuildingTypeEntity> BuildingTypes { get; set; } = null!;

    }
}
