using System;

namespace DynamoDBUnitOfWork.Exceptions
{
    internal sealed class MaximumTransactionOperationsException : Exception
    {
        public MaximumTransactionOperationsException()
            : base($"Cannot add more than {Constants.MaximumTransactionOperations} actions to the transaction. https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/Limits.html#limits-dynamodb-transactions")
        {

        }
    }
}
