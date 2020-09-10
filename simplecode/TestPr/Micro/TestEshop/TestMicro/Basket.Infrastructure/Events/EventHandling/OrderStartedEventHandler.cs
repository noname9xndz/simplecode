using Basket.Infrastructure.Events.Events;
using Basket.Infrastructure.Events.Services;
using Basket.Infrastructure.Extensions;
using Event.Bus.Services.Base.Interface;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Events.EventHandling
{
    public class OrderStartedEventHandler : IEventHandler<OrderStartedEvent>
    {
        private readonly IRedisBasketService _repository;
        private readonly ILogger<OrderStartedEventHandler> _logger;

        public OrderStartedEventHandler(
            IRedisBasketService repository,
            ILogger<OrderStartedEventHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(OrderStartedEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{CustomProgramExtension.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, CustomProgramExtension.AppName, @event);

                await _repository.DeleteBasketAsync(@event.UserId.ToString());
            }
        }
    }
}