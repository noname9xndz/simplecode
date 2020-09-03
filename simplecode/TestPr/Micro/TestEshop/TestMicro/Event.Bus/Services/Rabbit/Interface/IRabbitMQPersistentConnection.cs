using System;
using RabbitMQ.Client;

namespace Event.Bus.Services.Rabbit.Interface
{
    
    public interface IRabbitMQPersistentConnection: IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
