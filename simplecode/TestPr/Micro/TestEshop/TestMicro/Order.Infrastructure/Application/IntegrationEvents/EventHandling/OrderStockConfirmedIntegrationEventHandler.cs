﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Event.Bus.Extensions;
using Event.Bus.Services.Base.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Infrastructure.Application.Commands.Commands;
using Order.Infrastructure.Application.IntegrationEvents.Events.IntegrationResolve;
using Order.Infrastructure.Extensions;
using Serilog.Context;

namespace Order.Infrastructure.Application.IntegrationEvents.EventHandling
{
    public class OrderStockConfirmedIntegrationEventHandler : IEventHandler<OrderStockConfirmedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderStockConfirmedIntegrationEventHandler> _logger;

        public OrderStockConfirmedIntegrationEventHandler(
            IMediator mediator,
            ILogger<OrderStockConfirmedIntegrationEventHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderStockConfirmedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{CustomProgramExtension.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, CustomProgramExtension.AppName, @event);

                var command = new SetStockConfirmedOrderStatusCommand(@event.OrderId);

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    command.GetGenericTypeName(),
                    nameof(command.OrderNumber),
                    command.OrderNumber,
                    command);

                await _mediator.Send(command);
            }
        }
    }
}
