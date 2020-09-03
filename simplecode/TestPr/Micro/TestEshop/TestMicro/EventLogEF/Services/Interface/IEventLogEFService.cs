using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventLogEF.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage;

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
