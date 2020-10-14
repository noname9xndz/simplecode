using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDBUnitOfWork.Exceptions;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DynamoDBUnitOfWork
{
    public sealed class DynamoDBUnitOfWork : IDynamoUnitOfWork
    {
        private readonly IAmazonDynamoDB _client;

        private TransactWriteItem[] _operations;
        private UoWState _uoWState;
        private int _trackedOperations;

        public DynamoDBUnitOfWork(IAmazonDynamoDB client)
        {
            _uoWState = UoWState.New;
            _client = client;
        }

        public void AddOperation(TransactWriteItem transactWriteItem)
        {
            if (_uoWState != UoWState.Started)
            {
                throw new UnitOfWorkNotStartedException();
            }
            if (_trackedOperations >= Constants.MaximumTransactionOperations)
            {
                throw new MaximumTransactionOperationsException();
            }
            _operations[_trackedOperations++] = transactWriteItem;
        }

        public async Task<TransactWriteItemsResponse> Commit(string clientRequestToken, CancellationToken cancellationToken = default)
        {
            if (_uoWState != UoWState.Started)
                throw new UnitOfWorkNotStartedException();

            var request = new TransactWriteItemsRequest
            {
                TransactItems = _operations.ToList(),
                ClientRequestToken = clientRequestToken
            };
            _uoWState = UoWState.Committed;
            return await _client.TransactWriteItemsAsync(request, cancellationToken).ConfigureAwait(false);
        }

        public void Start()
        {
            if(_uoWState == UoWState.Started)
            {
                throw new OngoingTransactionException();
            }
            _operations = new TransactWriteItem[Constants.MaximumTransactionOperations];
            _uoWState = UoWState.Started;
            _trackedOperations = 0;
        }
    }

    internal enum UoWState
    {
        New,
        Started,
        Committed
    }
}
