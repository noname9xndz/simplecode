using System;
using System.Linq;
using System.Linq.Expressions;

namespace GenericRepository.Repositories.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for repository doing create, update and delete operations
    /// </summary>
    /// <typeparam name="TEnity">The datatable entity type</typeparam>
    public interface ICudRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add or create new entity to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entity">New entity</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds set of new entities to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entities">List of new entity</param>
        void AddMutil(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add or create new entity to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entity">New entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns task that is awaitable</returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds set of new entities to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entities">List of new entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns task that is awaitable</returns>
        Task AddMutilAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update modified record into database table
        /// </summary>
        /// <param name="entity">The modified entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Update list of modified record into database table
        /// </summary>
        /// <param name="entities">The modified entity list</param>
        void UpdateMutil(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete list of entities from database table
        /// </summary>
        /// <param name="entities">Entities that are going to be deleted</param>
        void PermanentlyDeleteMutil(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity from database table
        /// </summary>
        /// <param name="id">Entity id that is going to be deleted</param>
        /// <param name="cancellationToken">Cancellation token</param>
        void PermanentlyDelete(object id);

        /// <summary>
        /// Delete entity from database table
        /// </summary>
        /// <param name="entity">Entity that is going to be deleted</param>
        void PermanentlyDelete(TEntity entity);

        ///// <summary>
        ///// changed status : inactive entity from database table
        ///// </summary>
        ///// <param name="entity"></param>
        //Task TemporarilyDelete(TEntity entity);

        ///// <summary>
        ///// changed status : inactive entity from database table
        ///// </summary>
        ///// <param name="entity"></param>
        //Task TemporarilyDelete(object id);

        ///// <summary>
        ///// changed status : inactive entity from database table
        ///// </summary>
        ///// <param name="entity"></param>
        //Task TemporarilyDelete(IEnumerable<TEntity> entities);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task BulkInsert(IList<TEntity> items);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task BulkDelete(IList<TEntity> items);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<int> BulkDelete(IQueryable<TEntity> items);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task BulkUpdate(IList<TEntity> items);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task BulkUpdate(IQueryable<TEntity> items, Expression<Func<TEntity, TEntity>> value);
    }
}