using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Events.Events
{
    public class ProductRemovedEvent : ProductBaseEvent
    {
        public ProductRemovedEvent(int id, string name, string description, decimal price, string pictureFileName, string pictureUri, int catalogTypeId, int catalogBrandId, int availableStock, int restockThreshold, int maxStockThreshold, bool onReorder) : base(id, name, description, price, pictureFileName, pictureUri, catalogTypeId, catalogBrandId, availableStock, restockThreshold, maxStockThreshold, onReorder)
        {
        }
    }
}
