using System.Threading.Tasks;

namespace Event.Bus.Services.Base.Interface
{
    public interface IDynamicEventHandler
    {
        Task Handle(dynamic eventData);
    }
}