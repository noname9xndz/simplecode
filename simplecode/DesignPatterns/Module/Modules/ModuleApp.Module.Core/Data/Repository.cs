using ModuleApp.Infrastructure.Data;
using ModuleApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
