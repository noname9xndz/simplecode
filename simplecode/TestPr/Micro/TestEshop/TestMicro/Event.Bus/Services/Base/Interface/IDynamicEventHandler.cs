using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Event.Bus.Services.Base.Interface
{
    public interface IDynamicEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
