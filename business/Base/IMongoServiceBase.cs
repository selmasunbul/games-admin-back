﻿using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Base
{
    /// <summary>
    /// This interface is implemented by all repositories to ensure implementation of fixed methods.
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    /// <typeparam name="TPrimary">Primary key type of the entity</typeparam>

    public interface IMongoServiceBase<TEntity>
    {
        IMongoCollection<TEntity> _collection { get; }
        public string TableName { get; set; }

        void InitRepoWithTable();

        void InitRepoWithTable(string tableName);
        List<TEntity> GetAllListTable(FilterDefinition<TEntity> filter);
        void CreateTable(string tableName);

        #region Select/Get/Query

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <returns>List of all entities</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        IMongoQueryable<TEntity> GetAll();

        IMongoQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Used to run a query over entire entities.
        /// <see cref="UnitOfWorkAttribute"/> attribute is not always necessary (as opposite to <see cref="GetAll"/>)
        /// if <paramref name="queryMethod"/> finishes IQueryable with ToList, FirstOrDefault etc..
        /// </summary>
        /// <typeparam name="T">Type of return value of this method</typeparam>
        /// <param name="queryMethod">This method is used to query over entities</param>
        /// <returns>Query result</returns>
        T Query<T>(Func<IMongoQueryable<TEntity>, T> queryMethod);

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <returns>List of all entities</returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        TEntity Get(string id);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        Task<TEntity> GetAsync(string id);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Insert

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        string InsertAndGetId(TEntity entity);

        ///// <summary>
        ///// Inserts or updates given entity depending on Id's value.
        ///// </summary>
        ///// <param name="entity">Entity</param>
        //TEntity InsertOrUpdate(TEntity entity);

        ///// <summary>
        ///// Inserts or updates given entity depending on Id's value.
        ///// </summary>
        ///// <param name="entity">Entity</param>
        //Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        ///// <summary>
        ///// Inserts or updates given entity depending on Id's value.
        ///// Also returns Id of the entity.
        ///// It may require to save current unit of work
        ///// to be able to retrieve id.
        ///// </summary>
        ///// <param name="entity">Entity</param>
        ///// <returns>Id of the entity</returns>
        //TPrimary InsertOrUpdateAndGetId(TEntity entity);

        ///// <summary>
        ///// Inserts or updates given entity depending on Id's value.
        ///// Also returns Id of the entity.
        ///// It may require to save current unit of work
        ///// to be able to retrieve id.
        ///// </summary>
        ///// <param name="entity">Entity</param>
        ///// <returns>Id of the entity</returns>
        //Task<TPrimary> InsertOrUpdateAndGetIdAsync(TEntity entity);

        #endregion

        #region Update

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates an existing entity. 
        /// </summary>
        /// <param name="entity">Entity</param>
        Task UpdateAsync(TEntity entity);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        void Delete(string id);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        Task DeleteAsync(string id);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Aggregates

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        long Count();

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        Task<long> CountAsync();

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        long Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
        long Count(FilterDefinition<TEntity> filterDefinition);
        Task<long> CountAsync(FilterDefinition<TEntity> filterDefinition);
        void Delete(FilterDefinition<TEntity> filterDefinition);
        Task DeleteAsync(FilterDefinition<TEntity> filterDefinition);
        TEntity Get(FilterDefinition<TEntity> filterDefinition);
        Task<TEntity> GetAsync(FilterDefinition<TEntity> filterDefinition);
        List<TEntity> GetAllList(FilterDefinition<TEntity> filterDefinition);
        Task<List<TEntity>> GetAllListAsync(FilterDefinition<TEntity> filterDefinition);
        TEntity Update(string id, UpdateDefinition<TEntity> updateDefinition);
        Task<TEntity> UpdateAsync(string id, UpdateDefinition<TEntity> updateDefinition);
        Task<TEntity> UpdateAsync(FilterDefinition<TEntity> filterDefinition, UpdateDefinition<TEntity> updateDefinition);
        TEntity Update(FilterDefinition<TEntity> filterDefinition, UpdateDefinition<TEntity> updateDefinition);
        #endregion

    }
}
