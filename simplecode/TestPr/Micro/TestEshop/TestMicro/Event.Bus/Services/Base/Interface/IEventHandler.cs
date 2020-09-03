using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventLogEF.Models.Entities;

namespace Event.Bus.Services.Base.Interface
{
    public interface IEventHandler<in TIntegrationEvent> : IEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IEventHandler
    {
    }
}
