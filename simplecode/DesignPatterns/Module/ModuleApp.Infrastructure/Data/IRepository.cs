using ModuleApp.Infrastructure.Models;

namespace ModuleApp.Infrastructure.Data
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, long> where T : IEntityWithTypedId<long>
    {
    }
}