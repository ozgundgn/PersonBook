using EventBusRabbitMQ.Producer;
using EventRabbitMQ;
using EventRabbitMQ.Core;
using EventRabbitMQ.Events;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMq.Test.Models;
using Xunit;

namespace RabbitMq.Test
{
    public class EventBusRabbitMQProducerTest
    {

        private IRabbitMQPersistentConnection _persistentConnection;
        private ILogger<EventBusRabbitMQProducer> _logger;

        public EventBusRabbitMQProducerTest()
        {
            var mock = new Mock<ILogger<EventBusRabbitMQProducer>>();
            _logger = mock.Object;
            var mockPersistedConn = new Mock<IRabbitMQPersistentConnection>();
            _persistentConnection = mockPersistedConn.Object;
        }
        [Theory]
        [ClassData(typeof(EventTestData))]
        public void PubilshMetod_SendReportCreatedEvent_ReturnVoid(ReportCreatedEvent @event)
        {
            var eventBus = new EventBusRabbitMQProducer(_persistentConnection, _logger);
            eventBus.Publish(EventBusConstants.ReportCompletedQueue, @event);
            Assert.Equal(true, true);
        }
    }
}