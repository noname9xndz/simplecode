namespace GenericRepository
{
    using GenericRepository.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Contract that defines set of methods to create repositories and atomic operations
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///  Get readonly repository for the given database entity type
        /// </summary>
        /// <typeparam name="TEntity">Entity type for which repository required</typeparam>
        /// <returns>Readonly repository object</returns>
        IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Get repository that would allow CRUD operations over the given database entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type for which repository required</typeparam>
        /// <returns>Repository object</returns>
        ICrudRepository<TEntity> GetCrudRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Save database context changes into database
        /// </summary>
        /// <returns>Affected records count</returns>
        int SaveChanges();

        /// <summary>
        /// Save database context changes into database
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>Affected records count</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }

    /// <summary>
    /// Contract that defines set of methods to create repositories and atomic operations
    /// </summary>
    /// <typeparam name="TContext">Database context type</typeparam>
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        /// <summary>
        /// Gets database context object
        /// </summary>
        TContext Context { get; }
    }
}