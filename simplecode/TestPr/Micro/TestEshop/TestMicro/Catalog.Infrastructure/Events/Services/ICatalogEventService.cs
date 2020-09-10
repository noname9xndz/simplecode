using Catalog.Infrastructure.Models.Entities;
using EventLogEF.Models.Entities;
using System.Threading.Tasks;

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