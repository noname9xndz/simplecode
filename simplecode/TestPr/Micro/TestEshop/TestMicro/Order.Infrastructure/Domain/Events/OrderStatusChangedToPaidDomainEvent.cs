using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Order.Infrastructure.Domain.AggregatesModel.Entities;

namespace Order.Infrastructure.Domain.Events
{
    /// <summary>
    /// Event used when the order is paid
    /// </summary>
    public class OrderStatusChangedToPaidDomainEvent: INotification
    {
        public int OrderId { get; }
        public IEnumerable<OrderItem> OrderItems { get; }

        public OrderStatusChangedToPaidDomainEvent(int orderId,
            IEnumerable<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}
