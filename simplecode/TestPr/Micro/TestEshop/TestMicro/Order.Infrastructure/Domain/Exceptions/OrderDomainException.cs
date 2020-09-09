using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Infrastructure.Domain.Exceptions
{
    class OrderDomainException : Exception
    {
        public OrderDomainException()
        { }

        public OrderDomainException(string message)
            : base(message)
        { }

        public OrderDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
