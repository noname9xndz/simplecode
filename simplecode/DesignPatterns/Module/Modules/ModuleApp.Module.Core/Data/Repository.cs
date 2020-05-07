using ModuleApp.Infrastructure.Data;
using ModuleApp.Infrastructure.Models;

namespace ModuleApp.Module.Core.Data
{
    public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
       where T : class, IEntityWithTypedId<long>
    {
        public Repository(ModuleAppDbContext context) : base(context)
        {
        }
    }
}