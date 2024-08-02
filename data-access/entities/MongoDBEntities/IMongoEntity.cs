using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace data_access.entities.MongoDBEntities
{
    public interface IMongoEntity<T>
    {
        T OId { get; }
    }

    public interface IEntityCreatedAt
    {
        DateTime CreatedAt { get; }
    }

    public interface IEntity<T, K> : IMongoEntity<T>, IEntityCreatedAt
    {
        Guid CreatedBy { get; }
    }

    public interface IEntityLastModified<K>
    {
        Guid? LastModifiedBy { get; }
        DateTime? LastModifiedAt { get; }
    }

    public interface IEntityStatus
    {
        short Status { get; }
    }

    public interface IEntityDeletable
    {
        DateTime? DeletedAt { get; }
        bool IsDeleted { get; }
    }
}
