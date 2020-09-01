using System;
using Equinox.Domain.Core.Core.Messaging;

namespace Equinox.Domain.Events.Customer
{
    public class CustomerRemovedEvent : Event
    {
        public CustomerRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}