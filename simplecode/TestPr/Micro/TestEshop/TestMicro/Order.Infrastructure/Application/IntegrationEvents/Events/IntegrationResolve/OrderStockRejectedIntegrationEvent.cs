using System;
using System.Collections.Generic;
using System.Text;
using EventLogEF.Models.Entities;
using Order.Infrastructure.Models;
using Order.Infrastructure.Models.Event;

namespace Order.Infrastructure.Application.IntegrationEvents.Events.IntegrationResolve
{
    public class OrderStockRejectedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public List<ConfirmedOrderStockItem> OrderStockItems { get; }

        public OrderStockRejectedIntegrationEvent(int orderId,
            List<ConfirmedOrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
    }
}
