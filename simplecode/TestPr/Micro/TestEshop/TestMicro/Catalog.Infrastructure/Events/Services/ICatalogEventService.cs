using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Catalog.Infrastructure.Models.Entities;
using EventLogEF.Models.Entities;

namespace Catalog.Infrastructure.Events.Services
{
    public interface ICatalogEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
        Task<int> UpdateProductAsync(CatalogItem productToUpdate);
        Task<CatalogItem> CreateProductAsync(CatalogItem itemProduct);
        Task<int> DeleteProductAsync(int id);
    }
}
