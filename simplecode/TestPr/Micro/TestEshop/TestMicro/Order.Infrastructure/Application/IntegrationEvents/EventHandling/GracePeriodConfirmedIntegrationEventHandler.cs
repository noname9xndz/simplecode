using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Event.Bus.Extensions;
using Event.Bus.Services.Base.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Infrastructure.Application.Commands.Commands;
using Order.Infrastructure.Application.IntegrationEvents.Events;
using Order.Infrastructure.Application.IntegrationEvents.Events.IntegrationResolve;
using Order.Infrastructure.Extensions;
using Serilog.Context;

namespace Order.Infrastructure.Application.IntegrationEvents.EventHandling
{
    public class GracePeriodConfirmedIntegrationEventHandler : IEventHandler<GracePeriodConfirmedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GracePeriodConfirmedIntegrationEventHandler> _logger;

        public GracePeriodConfirmedIntegrationEventHandler(
            IMediator mediator,
            ILogger<GracePeriodConfirmedIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Event handler which confirms that the grace period
        /// has been completed and order will not initially be cancelled.
        /// Therefore, the order process continues for validation. 
        /// </summary>
        /// <param name="event">       
        /// </param>
        /// <returns></returns>
        public async Task Handle(GracePeriodConfirmedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{CustomProgramExtension.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, CustomProgramExtension.AppName, @event);

                var command = new SetAwaitingValidationOrderStatusCommand(@event.OrderId);

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
