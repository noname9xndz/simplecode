using Microsoft.Azure.ServiceBus;
using System;

namespace Event.Bus.Services.Interface
{
    public interface IAzureServiceBusPersisterConnection : IDisposable
    {
        ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder { get; }

        ITopicClient CreateModel();
    }
}