using EventLogEF.Models.Entities;

namespace Order.Infrastructure.Application.IntegrationEvents.Events.IntegrationResolve
{
    public class GracePeriodConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public GracePeriodConfirmedIntegrationEvent(int orderId) =>
            OrderId = orderId;
    }
}
