using EventLogEF.Models.Entities;
using System.Threading.Tasks;

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