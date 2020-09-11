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

namespace Order.Infrastructure.Application.DomainEventHandlers.OrderShipped
{
    public class OrderShippedDomainEventHandler
        : INotificationHandler<OrderShippedDomainEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerFactory _logger;
        private readonly IBuyerService _buyerService;
        private readonly IOrderEventService _orderEventService;


        public OrderShippedDomainEventHandler(
            IOrderService orderService, ILoggerFactory logger,
            IBuyerService buyerService,
            IOrderEventService orderEventService
        )
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _buyerService = buyerService ?? throw new ArgumentNullException(nameof(buyerService));
            _orderEventService = orderEventService ?? throw new ArgumentNullException(nameof(orderEventService));
        }

        public async Task Handle(OrderShippedDomainEvent orderShippedDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger<OrderShippedDomainEvent>()
                .LogTrace("Order with Id: {OrderId} has been successfully updated to status {Status} ({Id})",
                    orderShippedDomainEvent.Order.Id, nameof(OrderStatus.Shipped), OrderStatus.Shipped.Id);

            var order = await _orderService.GetAsync(orderShippedDomainEvent.Order.Id);
            var buyer = await _buyerService.FindByIdAsync(order.GetBuyerId.Value.ToString());

            var orderStatusChangedToShippedIntegrationEvent = new OrderStatusChangedToShippedIntegrationEvent(order.Id, order.OrderStatus.Name, buyer.Name);
            await _orderEventService.AddAndSaveEventAsync(orderStatusChangedToShippedIntegrationEvent);
        }
    }
}
