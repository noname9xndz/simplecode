using System;

namespace DynamoDBUnitOfWork.Exceptions
{
    public sealed class UnitOfWorkNotStartedException : Exception
    {
        public UnitOfWorkNotStartedException()
            : base("Attempt to operate on a unit of work that hasn't been started. Ensure you call `Start()` first. ")
        {
        }
    }
}
