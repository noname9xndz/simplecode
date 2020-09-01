using System.Threading.Tasks;

namespace Equinox.Domain.Core.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}