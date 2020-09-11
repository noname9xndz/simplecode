using System;
using System.Collections.Generic;
using System.Linq;
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
using Order.Infrastructure.Models;
using Order.Infrastructure.Models.Event;

namespace Order.Infrastructure.Application.DomainEventHandlers.OrderPaid
{
    public class OrderStatusChangedToPaidDomainEventHandler
                   : INotificationHandler<OrderStatusChangedToPaidDomainEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerFactory _logger;
        private readonly IBuyerService _buyerService;
        private readonly IOrderEventService _orderEventService;


        public OrderStatusChangedToPaidDomainEventHandler(
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

        public async Task Handle(OrderStatusChangedToPaidDomainEvent orderStatusChangedToPaidDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger<OrderStatusChangedToPaidDomainEventHandler>()
                .LogTrace("Order with Id: {OrderId} has been successfully updated to status {Status} ({Id})",
                    orderStatusChangedToPaidDomainEvent.OrderId, nameof(OrderStatus.Paid), OrderStatus.Paid.Id);

            var order = await _orderService.GetAsync(orderStatusChangedToPaidDomainEvent.OrderId);
            var buyer = await _buyerService.FindByIdAsync(order.GetBuyerId.Value.ToString());

            var orderStockList = orderStatusChangedToPaidDomainEvent.OrderItems
                .Select(orderItem => new OrderStockItem(orderItem.ProductId, orderItem.GetUnits()));

            var orderStatusChangedToPaidIntegrationEvent = new OrderStatusChangedToPaidIntegrationEvent(
                orderStatusChangedToPaidDomainEvent.OrderId,
                order.OrderStatus.Name,
                buyer.Name,
                orderStockList);

            await _orderEventService.AddAndSaveEventAsync(orderStatusChangedToPaidIntegrationEvent);
        }
    }
}
