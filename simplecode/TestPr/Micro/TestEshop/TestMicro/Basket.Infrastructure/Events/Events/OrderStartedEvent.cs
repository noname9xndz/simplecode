using EventLogEF.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Infrastructure.Events.Events
{
    // Integration Events notes: 
    // An Event is “something that has happened in the past”, therefore its name has to be   
    // An Integration Event is an event that can cause side effects to other microsrvices, Bounded-Contexts or external systems.
    public class OrderStartedEvent : IntegrationEvent
    {
        public string UserId { get; set; }

        public OrderStartedEvent(string userId)
            => UserId = userId;
    }
}
