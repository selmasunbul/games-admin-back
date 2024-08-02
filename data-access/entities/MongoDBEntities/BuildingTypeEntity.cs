using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.entities.MongoDBEntities
{
    public class BuildingTypeEntity : IMongoEntity<string>
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OId { get; set; }

        public string Name { get; set; }
    }
}
