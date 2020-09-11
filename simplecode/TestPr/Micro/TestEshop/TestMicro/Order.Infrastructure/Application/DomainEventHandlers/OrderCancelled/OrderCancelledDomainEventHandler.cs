using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Infrastructure.Application.IntegrationEvents.Events;
using Order.Infrastructure.Application.IntegrationEvents.Events.DomainResolve;
using Order.Infrastructure.Domain.AggregatesModel.Entities;
using Order.Infrastructure.Domain.Events;
using Order.Infrastructure.Domain.Services.Interface;

namespace Order.Infrastructure.Application.DomainEventHandlers.OrderCancelled
{
    public class OrderCancelledDomainEventHandler: INotificationHandler<OrderCancelledDomainEvent>
    {
        private readonly IOrderService _orderService;
        private readonly IBuyerService _buyerService;
        private readonly ILoggerFactory _logger;
        private readonly IOrderEventService _orderEventService;

        public OrderCancelledDomainEventHandler(
            IOrderService orderService,
            ILoggerFactory logger,
            IBuyerService buyerService,
            IOrderEventService orderEventService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _buyerService = buyerService ?? throw new ArgumentNullException(nameof(buyerService));
            _orderEventService = orderEventService;
        }

        public async Task Handle(OrderCancelledDomainEvent orderCancelledDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger<OrderCancelledDomainEvent>()
                .LogTrace("Order with Id: {OrderId} has been successfully updated to status {Status} ({Id})",
                    orderCancelledDomainEvent.Order.Id, nameof(OrderStatus.Cancelled), OrderStatus.Cancelled.Id);

            var order = await _orderService.GetAsync(orderCancelledDomainEvent.Order.Id);
            if (order != null)
            {
                var buyer = await _buyerService.FindByIdAsync(order.GetBuyerId.Value.ToString());

                var orderStatusChangedToCancelledIntegrationEvent = new OrderStatusChangedToCancelledIntegrationEvent(order.Id, order.OrderStatus.Name, buyer.Name);
                await _orderEventService.AddAndSaveEventAsync(orderStatusChangedToCancelledIntegrationEvent);
            }
        }
    }
}
