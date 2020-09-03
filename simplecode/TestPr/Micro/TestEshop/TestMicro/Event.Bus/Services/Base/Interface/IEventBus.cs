using System;
using System.Collections.Generic;
using System.Text;
using EventLogEF.Models.Entities;

namespace Event.Bus.Services.Base.Interface
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IEventHandler<T>
            where T : IntegrationEvent;
    }
}
