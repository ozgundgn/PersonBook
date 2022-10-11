using System;
using RabbitMQ.Client;

namespace EventRabbitMQ
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        public bool IsConnected { get; }
        bool TryConnect();

        public IModel CreateModel();
    }
}
