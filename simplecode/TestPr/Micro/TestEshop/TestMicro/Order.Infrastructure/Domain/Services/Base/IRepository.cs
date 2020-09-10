using System;
using System.Collections.Generic;
using System.Text;
using Order.Infrastructure.Domain.AggregatesModel.Base;

namespace Order.Infrastructure.Domain.Services.Base
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
