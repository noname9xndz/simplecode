using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventLogEF.Models.Entities;

namespace Catalog.Infrastructure.Events.Services
{
    public interface ICatalogEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
