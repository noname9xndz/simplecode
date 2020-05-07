namespace GenericRepository.Repositories.Interfaces
{
    /// <summary>
    /// CRUD repository interface
    /// </summary>
    public interface ICrudRepository<TEntity> : ICudRepository<TEntity>, IReadRepository<TEntity>
        where TEntity : class
    {
    }
}