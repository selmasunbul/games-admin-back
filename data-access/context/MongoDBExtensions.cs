using data_access.entities.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace data_access.context
{
    public static class MongoDBExtensions
    {
        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database)
        {
            var tableAttr = typeof(T).GetCustomAttribute(typeof(TableNameAttribute)) as TableNameAttribute;
            string? tableName;
            if (tableAttr != null)
                tableName = tableAttr.Name;
            else
                tableName = typeof(T).Name;

            return database.GetCollection<T>(tableName);
        }
    }

}
