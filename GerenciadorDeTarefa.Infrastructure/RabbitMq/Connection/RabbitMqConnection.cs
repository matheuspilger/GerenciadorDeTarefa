using GerenciadorDeTarefa.Domain.Configurations;
using GerenciadorDeTarefa.Infrastructure.RabbitMq.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace GerenciadorDeTarefa.Infrastructure.RabbitMq.Connection
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly RabbitMqConfiguration _configuration;
        private IChannel _channel = default!;

        public RabbitMqConnection(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            if (_channel is not null) return;
            var connectionFactory = new ConnectionFactory()
            {
                HostName = _configuration.HostName,
                Port = _configuration.Port,
                UserName = _configuration.UserName,
                Password = _configuration.Password
            };

            var connection = await connectionFactory.CreateConnectionAsync(cancellationToken: token);
            _channel = await connection.CreateChannelAsync(cancellationToken: token);
        }

        public async Task ConfigureAsync(QueueConfiguration queue, CancellationToken token)
        {
            await _channel.ExchangeDeclareAsync(exchange: queue.ExchangeName, type: queue.ExchangeType, cancellationToken: token);
            await _channel.QueueDeclareAsync(queue: queue.QueueName, durable: queue.Durable, exclusive: queue.Exclusive,
                    autoDelete: queue.AutoDelete, arguments: queue.Arguments, cancellationToken: token);
            await _channel.QueueBindAsync(exchange: queue.ExchangeName, queue: queue.QueueName, routingKey: queue.RoutingKey, cancellationToken: token);
        }

        public IChannel GetChannel() => _channel;
    }
}
