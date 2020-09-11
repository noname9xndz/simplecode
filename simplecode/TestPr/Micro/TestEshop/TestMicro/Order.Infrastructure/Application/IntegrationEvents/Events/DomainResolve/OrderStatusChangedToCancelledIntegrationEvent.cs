using EventLogEF.Models.Entities;

namespace Order.Infrastructure.Application.IntegrationEvents.Events.DomainResolve
{
    public class OrderStatusChangedToCancelledIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public string OrderStatus { get; }
        public string BuyerName { get; }

        public OrderStatusChangedToCancelledIntegrationEvent(int orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }
}
