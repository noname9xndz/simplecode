using System;

namespace DynamoDBUnitOfWork.Exceptions
{
    public sealed class OngoingTransactionException : Exception
    {
        public OngoingTransactionException()
            : base("Attempted to start a unit of work in an already started one.")
        {

        }
    }
}
