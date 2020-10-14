using Amazon.DynamoDBv2.Model;
using System.Threading;
using System.Threading.Tasks;

namespace DynamoDBUnitOfWork
{
    public interface IDynamoUnitOfWork
    {
        void Start();
        void AddOperation(TransactWriteItem transactWriteItem);
        Task<TransactWriteItemsResponse> Commit(string clientRequestToken, CancellationToken cancellationToken = default);
    }
}
