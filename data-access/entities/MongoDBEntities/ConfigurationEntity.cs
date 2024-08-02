using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace data_access.entities.MongoDBEntities
{
    public class ConfigurationEntity : IMongoEntity<string>
    {

        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OId { get; set; }

        public string BuildingType { get; set; }

        public int BuildingCost { get; set; }

        public int ConstructionTime { get; set; }
    }
}
