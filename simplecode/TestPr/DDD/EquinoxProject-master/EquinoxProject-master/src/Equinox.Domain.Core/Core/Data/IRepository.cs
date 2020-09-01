using System;
using Equinox.Domain.Core.Core.Domain;

namespace Equinox.Domain.Core.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}