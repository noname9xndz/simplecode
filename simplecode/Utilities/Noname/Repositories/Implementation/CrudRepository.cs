using GenericRepository.Extensions.BulkExtensions;

namespace GenericRepository.Repositories.Implementation
{
    using GenericRepository.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The repository class that perform CRUD operation
    /// </summary>
    /// <typeparam name="TEntity">The datatable entity type</typeparam>
    public class CrudRepository<TEntity> : ReadRepository<TEntity>, ICrudRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrudRepository.cs"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public CrudRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <inheritdoc />
        public virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        /// <inheritdoc />
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.DbSet.AddAsync(entity, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual void AddMutil(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }

        /// <inheritdoc />
        public virtual async Task AddMutilAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.DbSet.AddRangeAsync(entities, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual void PermanentlyDelete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = this.DbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                this.DbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                TEntity entity = this.DbSet.Find(id);
                if (entity != null)
                {
                    PermanentlyDelete(entity);
                }
            }
        }

        /// <inheritdoc />
        public virtual void PermanentlyDeleteMutil(IEnumerable<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
        }

        /// <inheritdoc />
        public virtual void PermanentlyDelete(TEntity entity)
        {
            if (this.DbContext.Entry(entity).State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }
            this.DbSet.Remove(entity);
        }

        //public virtual async Task TemporarilyDelete(TEntity entity)
        //{
        //    if (this.DbContext.Entry(entity).State == EntityState.Detached)
        //    {
        //        this.DbSet.Attach(entity);
        //    }
        //    this.DbSet.Remove(entity);
        //   // await Task.();
        //}

        //public virtual async Task TemporarilyDelete(object id)
        //{
        //    var typeInfo = typeof(TEntity).GetTypeInfo();
        //    var key = this.DbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
        //    var property = typeInfo.GetProperty(key?.Name);
        //    if (property != null)
        //    {
        //        var entity = Activator.CreateInstance<TEntity>();
        //        TrySetProperty(entity, "IsDeleted", false);
        //        property.SetValue(entity, id);
        //        this.DbContext.Entry(entity).State = EntityState.Modified;
        //    }
        //    else
        //    {
        //        TEntity entity = this.DbSet.Find(id);
        //        if (entity != null)
        //        {
        //            TemporarilyDelete(entity);
        //        }
        //    }
        //}

        //private bool TrySetProperty(object obj, string property, object value)
        //{
        //    var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
        //    if (prop != null && prop.CanWrite)
        //    {
        //        prop.SetValue(obj, value, null);
        //        return true;
        //    }
        //    return false;
        //}

        //public Task TemporarilyDelete(IEnumerable<TEntity> entities)
        //{
        //    throw new NotImplementedException();
        //}

        /// <inheritdoc />
        public virtual void Update(TEntity entity)
        {
            if (!this.DbContext.Entry(entity).IsKeySet)
            {
                throw new InvalidOperationException($"The primary key was not set on the entity class {entity.GetType().Name}");
            }

            this.DbSet.Update(entity);
        }

        /// <inheritdoc />
        public virtual void UpdateMutil(IEnumerable<TEntity> entities)
        {
            this.DbSet.UpdateRange(entities);
        }

        #region Bulk

        public virtual async Task BulkInsert(IList<TEntity> items)
        {
            await this.DbContext.BulkInsertAsync<TEntity>(items);
        }

        public virtual async Task BulkUpdate(IList<TEntity> items)
        {
            await this.DbContext.BulkUpdateAsync(items);
        }

        public virtual async Task BulkDelete(TEntity entity)
        {
            await this.DbContext.BulkDeleteAsync<TEntity>(new List<TEntity> { entity });
        }

        public virtual async Task BulkDelete(IList<TEntity> items)
        {
            if (items == null || !items.Any())
                return;
            await this.DbContext.BulkDeleteAsync(items);
        }

        public virtual async Task<int> BulkDelete(IQueryable<TEntity> items)
        {
            return await Task.FromResult(items.BatchDeleteAsync());
        }

        public virtual async Task BulkUpdate(IQueryable<TEntity> items, Expression<Func<TEntity, TEntity>> value)
        {
            await items.BatchUpdateAsync(value);
        }

        #endregion Bulk
    }
}