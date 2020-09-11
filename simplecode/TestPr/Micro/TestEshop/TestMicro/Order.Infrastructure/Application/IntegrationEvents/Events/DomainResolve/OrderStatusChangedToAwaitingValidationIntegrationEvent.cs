using System.Collections.Generic;
using EventLogEF.Models.Entities;
using Order.Infrastructure.Models;
using Order.Infrastructure.Models.Event;

namespace Order.Infrastructure.Application.IntegrationEvents.Events.DomainResolve
{
    public class OrderStatusChangedToAwaitingValidationIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public string OrderStatus { get; }
        public string BuyerName { get; }
        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToAwaitingValidationIntegrationEvent(int orderId, string orderStatus, string buyerName,
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }
}
