using System.Threading;
using MediatR;
using System.Threading.Tasks;
using Order.Infrastructure.Domain.Events;

namespace Order.Infrastructure.Application.DomainEventHandlers.OrderStarted
{
   // public class SendEmailToCustomerWhenOrderStartedDomainEventHandler: IAsyncNotificationHandler<OrderStartedDomainEvent>
    public class SendEmailToCustomerWhenOrderStartedDomainEventHandler: INotificationHandler<OrderStartedDomainEvent>
    {
        public SendEmailToCustomerWhenOrderStartedDomainEventHandler()
        {

        }

        //public async Task Handle(OrderStartedDomainEvent orderNotification)
        //{
        //    //TBD - Send email logic
        //}

        public async Task Handle(OrderStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            //TBD - Send email logic
        }
    }
}
