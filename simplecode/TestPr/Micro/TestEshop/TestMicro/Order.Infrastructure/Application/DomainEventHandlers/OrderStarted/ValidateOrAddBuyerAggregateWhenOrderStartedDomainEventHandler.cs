using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Infrastructure.Application.IntegrationEvents.Events;
using Order.Infrastructure.Domain.AggregatesModel.Entities;
using Order.Infrastructure.Domain.Events;
using Order.Infrastructure.Domain.Services.Interface;

namespace Order.Infrastructure.Application.DomainEventHandlers.OrderStarted
{
    public class ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler : INotificationHandler<OrderStartedDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private readonly IBuyerService _buyerService;
        private readonly IOrderEventService _orderEventService;

        public ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler(
            ILoggerFactory logger,
            IBuyerService buyerService,
            IOrderEventService orderEventService)
        {
            _buyerService = buyerService ?? throw new ArgumentNullException(nameof(buyerService));
            _orderEventService = orderEventService ?? throw new ArgumentNullException(nameof(orderEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderStartedDomainEvent orderStartedEvent, CancellationToken cancellationToken)
        {
            var cardTypeId = (orderStartedEvent.CardTypeId != 0) ? orderStartedEvent.CardTypeId : 1;
            var buyer = await _buyerService.FindAsync(orderStartedEvent.UserId);
            bool buyerOriginallyExisted = (buyer != null);

            if (!buyerOriginallyExisted)
            {
                buyer = new Buyer(orderStartedEvent.UserId, orderStartedEvent.UserName);
            }

            buyer.VerifyOrAddPaymentMethod(cardTypeId,
                                           $"Payment Method on {DateTime.UtcNow}",
                                           orderStartedEvent.CardNumber,
                                           orderStartedEvent.CardSecurityNumber,
                                           orderStartedEvent.CardHolderName,
                                           orderStartedEvent.CardExpiration,
                                           orderStartedEvent.Order.Id);

            var buyerUpdated = buyerOriginallyExisted ?
                _buyerService.Update(buyer) :
                _buyerService.Add(buyer);

            await _buyerService.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            var orderStatusChangedTosubmittedIntegrationEvent = new OrderStatusChangedToSubmittedIntegrationEvent(orderStartedEvent.Order.Id, orderStartedEvent.Order.OrderStatus.Name, buyer.Name);
            await _orderEventService.AddAndSaveEventAsync(orderStatusChangedTosubmittedIntegrationEvent);
            _logger.CreateLogger<ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler>()
                .LogTrace("Buyer {BuyerId} and related payment method were validated or updated for orderId: {OrderId}.",
                    buyerUpdated.Id, orderStartedEvent.Order.Id);
        }
    }
}
