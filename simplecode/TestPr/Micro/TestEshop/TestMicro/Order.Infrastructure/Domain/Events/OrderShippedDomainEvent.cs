using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Order.Infrastructure.Domain.Events
{
    public class OrderShippedDomainEvent : INotification
    {
        public AggregatesModel.Entities.Order Order { get; }

        public OrderShippedDomainEvent(AggregatesModel.Entities.Order order)
        {
            Order = order;
        }
    }
}
