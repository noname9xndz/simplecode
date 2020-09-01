using System;
using Equinox.Domain.Core.Core.Messaging;

namespace Equinox.Domain.Events.Product
{
    public class ProductRemovedEvent : Event
    {
        public ProductRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}