using System.Text;
using EventRabbitMQ;
using EventRabbitMQ.Events.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IBasicProperties = RabbitMQ.Client.IBasicProperties;

namespace EventBusRabbitMQ.Producer
{
    public class EventBusRabbitMQProducer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQProducer> _logger;

        public EventBusRabbitMQProducer(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusRabbitMQProducer> logger)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
        }

        public void Publish(string queueName, IEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            try
            {
                using (var channel = _persistentConnection.CreateModel())
                {

                    channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);


                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.DeliveryMode = 2;
                    channel.ConfirmSelect();
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queueName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                    channel.WaitForConfirmsOrDie();
                    channel.BasicAcks += (sernder, eventArgs) =>
                    {
                        Console.WriteLine("Sent RabbitMQ");
                    };
                }

            }
            catch (Exception)
            {
                _logger.LogWarning("A RabbitMQ has some problems about publishing.");
            }
        }
    }
}
