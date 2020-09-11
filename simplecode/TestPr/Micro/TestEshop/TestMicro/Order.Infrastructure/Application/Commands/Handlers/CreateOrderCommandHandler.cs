using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Infrastructure.Application.Commands.Commands;
using Order.Infrastructure.Application.IntegrationEvents.Events;
using Order.Infrastructure.Application.IntegrationEvents.Events.DomainResolve;
using Order.Infrastructure.Domain.Services.Interface;

namespace Order.Infrastructure.Application.Commands.Handlers
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderService _orderService;
        private readonly IOrderEventService _orderingEventService;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public CreateOrderCommandHandler(
            IOrderEventService orderingEventService,
            IOrderService orderService,
            ILogger<CreateOrderCommandHandler> logger)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _orderingEventService = orderingEventService ?? throw new ArgumentNullException(nameof(orderingEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateOrderCommand message, CancellationToken cancellationToken)
        {
            // Add Integration event to clean the basket
            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(message.UserId);
            await _orderingEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

            // Add/Update the Buyer AggregateRoot
            // DDD patterns comment: Add child entities and value-objects through the Order Aggregate-Root
            // methods and constructor so validations, invariants and business logic 
            // make sure that consistency is preserved across the whole aggregate
            var address = new Domain.AggregatesModel.Entities.Address(message.Street, message.City, message.State, message.Country, message.ZipCode);
            var order = new Domain.AggregatesModel.Entities.Order(message.UserId, message.UserName, address, message.CardTypeId, message.CardNumber, message.CardSecurityNumber, message.CardHolderName, message.CardExpiration);

            foreach (var item in message.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
            }

            _logger.LogInformation("----- Creating Order - Order: {@Order}", order);

            _orderService.Add(order);

            return await _orderService.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);
        }
    }
}
