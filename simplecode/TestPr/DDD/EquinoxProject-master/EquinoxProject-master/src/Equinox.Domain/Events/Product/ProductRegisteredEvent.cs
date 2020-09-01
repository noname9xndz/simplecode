using System;
using Equinox.Domain.Core.Core.Messaging;

namespace Equinox.Domain.Events.Product
{
    public class ProductRegisteredEvent : Event
    {
        public ProductRegisteredEvent(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }
    }
}