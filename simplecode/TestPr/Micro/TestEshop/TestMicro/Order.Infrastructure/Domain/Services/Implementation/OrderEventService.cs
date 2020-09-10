using Order.Infrastructure.Domain.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Event.Bus.Services.Base.Interface;
using EventLogEF.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Infrastructure.Context;
using EventLogEF.Services.Interface;
using Order.Infrastructure.Extensions;

namespace Order.Infrastructure.Domain.Services.Implementation
{
    public class OrderEventService : IOrderEventService
    {
        private readonly Func<DbConnection, IEventLogEFService> _eventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly OrderDbContext _orderingContext;
        private readonly IEventLogEFService _eventLogService;
        private readonly ILogger<OrderEventService> _logger;

        public OrderEventService(IEventBus eventBus,
            OrderDbContext orderingContext,
            Func<DbConnection, IEventLogEFService> eventLogServiceFactory,
            ILogger<OrderEventService> logger)
        {
            _orderingContext = orderingContext ?? throw new ArgumentNullException(nameof(orderingContext));
            _eventLogServiceFactory = eventLogServiceFactory ?? throw new ArgumentNullException(nameof(eventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _eventLogServiceFactory(_orderingContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})",
                    logEvt.EventId, CustomProgramExtension.AppName, logEvt.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}",
                        logEvt.EventId, CustomProgramExtension.AppName);

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await _eventLogService.SaveEventAsync(evt, _orderingContext.GetCurrentTransaction());
        }
    }
}
