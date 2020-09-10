using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Order.Infrastructure.Domain.Events
{
    public class OrderCancelledDomainEvent : INotification
    {
        public AggregatesModel.Entities.Order Order { get; }

        public OrderCancelledDomainEvent(AggregatesModel.Entities.Order order)
        {
            Order = order;
        }
    }
}
