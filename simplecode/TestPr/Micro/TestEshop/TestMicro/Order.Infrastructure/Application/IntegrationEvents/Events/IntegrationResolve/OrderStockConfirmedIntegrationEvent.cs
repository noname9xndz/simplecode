using System;
using System.Collections.Generic;
using System.Text;
using EventLogEF.Models.Entities;

namespace Order.Infrastructure.Application.IntegrationEvents.Events.IntegrationResolve
{
    public class OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderStockConfirmedIntegrationEvent(int orderId) => OrderId = orderId;
    }
}
