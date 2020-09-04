using System;
using System.Collections.Generic;
using System.Text;
using EventLogEF.Models.Entities;

namespace Catalog.Infrastructure.Events.Events
{
    public class ProductPriceChangedEvent : IntegrationEvent
    {
        public int ProductId { get; private set; }

        public decimal NewPrice { get; private set; }

        public decimal OldPrice { get; private set; }

        public ProductPriceChangedEvent(int productId, decimal newPrice, decimal oldPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
