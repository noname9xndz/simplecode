using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Infrastructure.Models.Entities;
using EventLogEF.Models.Entities;

namespace Catalog.Infrastructure.Events.Events
{
    public class ProductCreatedEvent : ProductBaseEvent
    {
        public ProductCreatedEvent(int id, string name, string description, decimal price, string pictureFileName, string pictureUri, int catalogTypeId, int catalogBrandId, int availableStock, int restockThreshold, int maxStockThreshold, bool onReorder) : base(id, name, description, price, pictureFileName, pictureUri, catalogTypeId, catalogBrandId, availableStock, restockThreshold, maxStockThreshold, onReorder)
        {
        }
    }
}
