using EventLogEF.Models.Entities;

namespace Order.Infrastructure.Application.IntegrationEvents.Events.IntegrationResolve
{
    public class OrderPaymentSucceededIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderPaymentSucceededIntegrationEvent(int orderId) => OrderId = orderId;
    }
}
