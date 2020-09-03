using System;
using System.Collections.Generic;
using System.Text;
using Event.Bus.Models;
using EventLogEF.Models.Entities;

namespace Event.Bus.Services.Base.Interface
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        void AddDynamicSubscription<TH>(string eventName)
            where TH : IDynamicEventHandler;

        void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IEventHandler<T>;

        void RemoveSubscription<T, TH>()
            where TH : IEventHandler<T>
            where T : IntegrationEvent;
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicEventHandler;

        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        bool HasSubscriptionsForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();
    }
}
