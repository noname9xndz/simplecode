using EventLogEF.Models.Entities;

namespace Order.Infrastructure.Application.IntegrationEvents.Events.IntegrationResolve
{
    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderPaymentFailedIntegrationEvent(int orderId) => OrderId = orderId;
    }
}
