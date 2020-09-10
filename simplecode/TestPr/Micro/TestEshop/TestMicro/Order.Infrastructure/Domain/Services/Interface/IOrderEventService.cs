using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventLogEF.Models.Entities;

namespace Order.Infrastructure.Domain.Services.Interface
{
    public interface IOrderEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
