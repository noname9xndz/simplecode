using RabbitMQ.Client;
using System;

namespace Event.Bus.Services.Rabbit.Interface
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}