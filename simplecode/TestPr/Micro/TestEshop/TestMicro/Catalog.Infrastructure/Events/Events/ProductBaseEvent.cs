using System;
using System.Collections.Generic;
using System.Text;
using EventLogEF.Models.Entities;

namespace Catalog.Infrastructure.Events.Events
{
    public class ProductBaseEvent : IntegrationEvent
    {
        public ProductBaseEvent(int id, string name, string description, decimal price,
            string pictureFileName,
            string pictureUri,
            int catalogTypeId,
            int catalogBrandId,
            int availableStock,
            int restockThreshold,
            int maxStockThreshold,
            bool onReorder
        ) : base()
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            PictureUri = pictureUri;
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            AvailableStock = availableStock;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
            OnReorder = onReorder;
        }


        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        public int CatalogTypeId { get; set; }

        public int CatalogBrandId { get; set; }

        public int AvailableStock { get; set; }

        public int RestockThreshold { get; set; }

        public int MaxStockThreshold { get; set; }

        public bool OnReorder { get; set; }
    }
}
