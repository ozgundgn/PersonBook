using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Net.Sockets;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace EventRabbitMQ
{
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
        private bool _disposed;

        public bool IsConnected
        {
            get
            {
                return _connection != null && !_disposed && _connection.IsOpen;
            }
        }

        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogger<DefaultRabbitMQPersistentConnection> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ Client is trying to connect.");

            try
            {
                _connection = _connectionFactory.CreateConnection();
                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutDown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    _logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{Hostname}' and is subscribed to failure events.", _connection.ClientProvidedName);
                    return true;
                }
                else
                {
                    _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened.", _connection.ClientProvidedName);
                    return false;
                }

            }
            catch (Exception)
            {
                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened.", _connection.ClientProvidedName);
                return false ;
            }

        }

        private void OnConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            if (!_disposed) return;

            _logger.LogWarning("A RabbitMQ connections is shutdown. Trying to reconnect.");

            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (!_disposed) return;

            _logger.LogWarning("A RabbitMQ connections is shutdown. Trying to reconnect.");

            TryConnect();
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (!_disposed) return;

            _logger.LogWarning("A RabbitMQ connections is shutdown. Trying to reconnect.");

            TryConnect();

        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are avaliable.");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
            try
            {
                _connection.Dispose();
            }
            catch (IOException e)
            {
                _logger.LogCritical(e.ToString());
            }
        }
    }
}
