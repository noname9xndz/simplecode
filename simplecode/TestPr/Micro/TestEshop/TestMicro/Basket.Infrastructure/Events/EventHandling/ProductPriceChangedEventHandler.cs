using Basket.Infrastructure.Events.Events;
using Basket.Infrastructure.Events.Services;
using Basket.Infrastructure.Extensions;
using Basket.Infrastructure.Models;
using Event.Bus.Services.Base.Interface;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Events.EventHandling
{
    public class ProductPriceChangedEventHandler : IEventHandler<ProductPriceChangedEvent>
    {
        private readonly ILogger<ProductPriceChangedEventHandler> _logger;
        private readonly IRedisBasketService _repository;

        public ProductPriceChangedEventHandler(
            ILogger<ProductPriceChangedEventHandler> logger,
            IRedisBasketService repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(ProductPriceChangedEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{CustomProgramExtension.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, CustomProgramExtension.AppName, @event);

                var userIds = _repository.GetUsers();

                foreach (var id in userIds)
                {
                    var basket = await _repository.GetBasketAsync(id);

                    await UpdatePriceInBasketItems(@event.ProductId, @event.NewPrice, @event.OldPrice, basket);
                }
            }
        }

        private async Task UpdatePriceInBasketItems(int productId, decimal newPrice, decimal oldPrice, CustomerBasket basket)
        {
            var itemsToUpdate = basket?.Items?.Where(x => x.ProductId == productId).ToList();

            if (itemsToUpdate != null)
            {
                _logger.LogInformation("----- ProductPriceChangedEventHandler - Updating items in basket for user: {BuyerId} ({@Items})", basket.BuyerId, itemsToUpdate);

                foreach (var item in itemsToUpdate)
                {
                    if (item.UnitPrice == oldPrice)
                    {
                        var originalPrice = item.UnitPrice;
                        item.UnitPrice = newPrice;
                        item.OldUnitPrice = originalPrice;
                    }
                }
                await _repository.UpdateBasketAsync(basket);
            }
        }
    }
}