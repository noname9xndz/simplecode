using EventLogEF.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventLogEF.Services.Interface
{
    public interface IEventLogEFService
    {
        Task<IEnumerable<EventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);

        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);

        Task MarkEventAsPublishedAsync(Guid eventId);

        Task MarkEventAsInProgressAsync(Guid eventId);

        Task MarkEventAsFailedAsync(Guid eventId);
    }
}