using data_access.Abstract;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using data_access.context;
using data_access.entities.MongoDBEntities;
using MongoDB.Bson;
namespace Business.Base
{
    public class MongoServiceBase<TEntity> : IMongoServiceBase<TEntity>
    where TEntity : IMongoEntity<string>
    {
        private readonly IMongoDatabase _database;
        public string TableName { get; set; }
        public IMongoCollection<TEntity> _collection { get; set; }

        public MongoServiceBase(MongoDbContext context)
        {
            this._database = context.MongoDatabase;
            this._collection = _database.GetCollection<TEntity>();
        }
        public void InitRepoWithTable()
        {
            this._collection = _database.GetCollection<TEntity>(TableName);
        }

        public void InitRepoWithTable(string tableName)
        {
            this._collection = _database.GetCollection<TEntity>(tableName);
        }

        public List<TEntity> GetAllListTable(FilterDefinition<TEntity> filter)
        {
            return _collection.Find(filter).ToList();
        }


        public void CreateTable(string tableName)
        {
            _database.CreateCollection(tableName);
            //this._collection = _database.GetCollection<TEntity>(tableName);
        }

        public IMongoQueryable<TEntity> GetAll()
        {
            return _collection.AsQueryable();
        }

        public IMongoQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.AsQueryable().Where(predicate);
        }

        public T Query<T>(Func<IMongoQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public long Count()
        {
            return Count(x => true);
        }

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.CountDocuments(predicate);
        }

        public long Count(FilterDefinition<TEntity> filterDefinition)
        {
            return _collection.CountDocuments(filterDefinition);
        }

        public Task<long> CountAsync()
        {
            return CountAsync(x => true);
        }

        public Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.CountDocumentsAsync(predicate);
        }

        public Task<long> CountAsync(FilterDefinition<TEntity> filterDefinition)
        {
            return _collection.CountDocumentsAsync(filterDefinition);
        }

        public void Delete(TEntity entity)
        {
            Delete(entity.OId);
        }

        public void Delete(string id)
        {
            Delete(x => x.OId.Equals(id));
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _collection.DeleteMany(predicate);
        }

        public void Delete(FilterDefinition<TEntity> filterDefinition)
        {
            _collection.DeleteMany(filterDefinition);
        }

        public Task DeleteAsync(TEntity entity)
        {
            return DeleteAsync(entity.OId);
        }

        public Task DeleteAsync(string id)
        {
            return DeleteAsync(x => x.OId.Equals(id));
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.DeleteManyAsync(predicate);
        }

        public Task DeleteAsync(FilterDefinition<TEntity> filterDefinition)
        {
            return _collection.DeleteManyAsync(filterDefinition);
        }

        public TEntity Get(string id)
        {
            return Get(x => x.OId.Equals(id));
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).FirstOrDefault();
        }

        public TEntity Get(FilterDefinition<TEntity> filterDefinition)
        {
            return _collection.Find(filterDefinition).FirstOrDefault();
        }

        public Task<TEntity> GetAsync(string id)
        {
            return GetAsync(x => x.OId.Equals(id));
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            // TODO: FindAsync veya FirstOrDefaultAsync hangisi kullanilmali
            return _collection.Find(predicate).FirstOrDefaultAsync();
        }

        public Task<TEntity> GetAsync(FilterDefinition<TEntity> filterDefinition)
        {
            // TODO: FindAsync veya FirstOrDefaultAsync hangisi kullanilmali
            return _collection.Find(filterDefinition).FirstOrDefaultAsync();
        }

        public List<TEntity> GetAllList()
        {
            return GetAllList(x => true);
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).ToList();
        }

        public List<TEntity> GetAllList(FilterDefinition<TEntity> filterDefinition)
        {
            return _collection.Find(filterDefinition).ToList();
        }

        public Task<List<TEntity>> GetAllListAsync()
        {
            return GetAllListAsync(p => true);
        }

        public Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _collection.Find(predicate).ToListAsync();
        }

        public Task<List<TEntity>> GetAllListAsync(FilterDefinition<TEntity> filterDefinition)
        {
            return _collection.Find(filterDefinition).ToListAsync();
        }

        public TEntity Insert(TEntity entity)
        {
            // TODO: Id setleniyor mu kontrol edilecek.
            _collection.InsertOne(entity);

            return entity;
        }

        public string InsertAndGetId(TEntity entity)
        {
            // TODO: Id setleniyor mu kontrol edilecek.
            return Insert(entity).OId;
        }

        public Task InsertAsync(TEntity entity)
        {
            // TODO: Id setleniyor mu kontrol edilecek.
            return _collection.InsertOneAsync(entity);
        }

        public TEntity Update(TEntity entity)
        {
            _collection.ReplaceOne(x => x.OId.Equals(entity.OId), entity);

            return entity;
        }

        public TEntity Update(string id, UpdateDefinition<TEntity> updateDefinition)
        {
            var option = new FindOneAndUpdateOptions<TEntity>
            {
                IsUpsert = false,
                ReturnDocument = ReturnDocument.After
            };

            var result = _collection.FindOneAndUpdate<TEntity>(x => x.OId.Equals(id), updateDefinition, option);

            return result;
        }

        public TEntity Update(FilterDefinition<TEntity> filterDefinition, UpdateDefinition<TEntity> updateDefinition)
        {
            var option = new FindOneAndUpdateOptions<TEntity>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var result = _collection.FindOneAndUpdate<TEntity>(filterDefinition, updateDefinition, option);

            return result;
        }

        public Task UpdateAsync(TEntity entity)
        {
            return _collection.ReplaceOneAsync(x => x.OId.Equals(entity.OId), entity);
        }

        public Task<TEntity> UpdateAsync(string id, UpdateDefinition<TEntity> updateDefinition)
        {
            var option = new FindOneAndUpdateOptions<TEntity>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var result = _collection.FindOneAndUpdateAsync<TEntity>(x => x.OId.Equals(id), updateDefinition, option);

            return result;
        }

        public Task<TEntity> UpdateAsync(FilterDefinition<TEntity> filterDefinition, UpdateDefinition<TEntity> updateDefinition)
        {
            var option = new FindOneAndUpdateOptions<TEntity>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var result = _collection.FindOneAndUpdateAsync<TEntity>(filterDefinition, updateDefinition, option);

            return result;
        }

  
    }
}
