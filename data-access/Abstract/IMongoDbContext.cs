using data_access.entities.MongoDBEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Abstract
{
    public interface IMeasurementDbContext
    {
        MongoClient MongoClient { get; }
        IMongoDatabase MongoDatabase { get; }

        IMongoCollection<ConfigurationEntity> Configurations { get; set; }
    }
}
