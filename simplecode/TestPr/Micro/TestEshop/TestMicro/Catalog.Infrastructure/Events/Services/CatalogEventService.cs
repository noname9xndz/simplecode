using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Events.Events;
using Catalog.Infrastructure.Extensions;
using Catalog.Infrastructure.Ioc;
using Catalog.Infrastructure.Models.Entities;
using Event.Bus.Services.Base.Interface;
using EventLogEF.Models.Entities;
using EventLogEF.Services.Interface;
using EventLogEF.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Events.Services
{

    public class CatalogEventService : ICatalogEventService, IDisposable
    {
        private readonly Func<DbConnection, IEventLogEFService> _eventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly CatalogDbContext _catalogContext;
        private readonly IEventLogEFService _eventLogService;
        private readonly ILogger<CatalogEventService> _logger;
        private volatile bool disposedValue;

        public CatalogEventService(
            ILogger<CatalogEventService> logger,
            IEventBus eventBus,
            CatalogDbContext catalogContext,
            Func<DbConnection, IEventLogEFService> eventLogServiceFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
            _eventLogServiceFactory = eventLogServiceFactory ?? throw new ArgumentNullException(nameof(eventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _eventLogServiceFactory(_catalogContext.Database.GetDbConnection());
        }


        public async Task<int> UpdateProductAsync(CatalogItem productToUpdate)
        {
            var product = await _catalogContext.CatalogItems.SingleOrDefaultAsync(i => i.Id == productToUpdate.Id);

            if (product == null)
            {
                return 0;
            }

            //            var oldPrice = item.Price;
            //            var raiseProductPriceChangedEvent = oldPrice != productToUpdate.Price;

            // Update current product
            product = productToUpdate;
            _catalogContext.CatalogItems.Update(product);

            // Save product's data and publish integration event through the Event Bus if price has changed

            var productUpdatedEvent = new ProductUpdatedEvent(product.Id, product.Name, product.Description,
                product.Price, product.PictureFileName, product.PictureUri, product.CatalogTypeId, product.CatalogBrandId, product.AvailableStock,
                product.RestockThreshold, product.MaxStockThreshold, product.OnReorder);

            // Achieving atomicity between original Catalog database operation and the IntegrationEventLog thanks to a local transaction
            await SaveEventAndCatalogContextChangesAsync(productUpdatedEvent);

            // Publish through the Event Bus and mark the saved event as published
            await PublishThroughEventBusAsync(productUpdatedEvent);

            //           if (raiseProductPriceChangedEvent) 
            //            {
            //               var priceChangedEvent = new ProductPriceChangedEvent(item.Id, productToUpdate.Price, oldPrice);
            //
            //                await SaveEventAndCatalogContextChangesAsync(priceChangedEvent);
            //
            //                await PublishThroughEventBusAsync(priceChangedEvent);
            //            }
            //            else
            //            {
            //                await _catalogContext.SaveChangesAsync();
            //            }

            return 1;
        }

        public async Task<CatalogItem> CreateProductAsync(CatalogItem itemProduct)
        {
            try
            {
                var product = new CatalogItem
                {
                    CatalogBrandId = itemProduct.CatalogBrandId,
                    CatalogTypeId = itemProduct.CatalogTypeId,
                    Description = itemProduct.Description,
                    Name = itemProduct.Name,
                    PictureFileName = itemProduct.PictureFileName,
                    Price = itemProduct.Price
                };


                await _catalogContext.CatalogItems.AddAsync(product);

                //Create Integration Event to be published through the Event Bus
                var productCreatedEvent = new ProductCreatedEvent(product.Id, product.Name, product.Description,
                    product.Price, product.PictureFileName, product.PictureUri, product.CatalogTypeId, product.CatalogBrandId, product.AvailableStock,
                    product.RestockThreshold, product.MaxStockThreshold, product.OnReorder);

                // Achieving atomicity between original Catalog database operation and the IntegrationEventLog thanks to a local transaction
                await SaveEventAndCatalogContextChangesAsync(productCreatedEvent);

                // Publish through the Event Bus and mark the saved event as published
                await PublishThroughEventBusAsync(productCreatedEvent);

                return product;
            }
            catch (System.Exception e)
            {
                return null;
            }

        }

        public async Task<int> DeleteProductAsync(int id)
        {
            var product = _catalogContext.CatalogItems.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return 0;
            }

            _catalogContext.CatalogItems.Remove(product);

            var productRemovedEvent = new ProductRemovedEvent(product.Id, product.Name, product.Description,
                product.Price, product.PictureFileName, product.PictureUri, product.CatalogTypeId, product.CatalogBrandId, product.AvailableStock,
                product.RestockThreshold, product.MaxStockThreshold, product.OnReorder);

            // Achieving atomicity between original Catalog database operation and the IntegrationEventLog thanks to a local transaction
            await SaveEventAndCatalogContextChangesAsync(productRemovedEvent);

            await PublishThroughEventBusAsync(productRemovedEvent);

            return 1;
        }

        //todo resolve result for SaveEventAndCatalogContextChangesAsync : 1 or 0
        #region Handle Event

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId_published} from {AppName} - ({@IntegrationEvent})", evt.Id, CustomProgramExtension.AppName, evt);

                await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", evt.Id, CustomProgramExtension.AppName, evt);
                await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }


        public async Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- CatalogIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_catalogContext).ExecuteAsync(async () =>
            {
                // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                await _catalogContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(evt, _catalogContext.Database.CurrentTransaction);
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    (_eventLogService as IDisposable)?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
