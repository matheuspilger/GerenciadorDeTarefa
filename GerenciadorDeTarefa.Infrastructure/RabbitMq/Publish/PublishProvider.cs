using GerenciadorDeTarefa.Domain.Configurations;
using GerenciadorDeTarefa.Domain.Events.Base;
using GerenciadorDeTarefa.Domain.Helpers;
using GerenciadorDeTarefa.Infrastructure.RabbitMq.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GerenciadorDeTarefa.Infrastructure.RabbitMq.Publish
{
    public class PublishProvider<T> : IPublishProvider<T> where T : EventBase
    {
        private QueueConfiguration _queueConfiguration = default!;
        private PublishConfiguration<BasicProperties> _publishConfiguration = default!;
        private readonly IConfiguration _configuration;
        private readonly IRabbitMqConnection _rabbitMqConnection;
        private readonly ILogger<PublishProvider<T>> _logger;

        public PublishProvider(
            IConfiguration configuration,
            IRabbitMqConnection rabbitMqConnection,
            ILogger<PublishProvider<T>> logger)
        {
            _configuration = configuration;
            _rabbitMqConnection = rabbitMqConnection;
            _logger = logger;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            try
            {
                var queueConfiguration = _configuration.GetSection(typeof(T).GetQueueConfig()).Get<QueueConfiguration>()
                    ?? throw new Exception($"Configuração da queue {typeof(T).Name} não localizada.");
                var publishConfiguration = _configuration.GetSection(typeof(T).GetPublishConfig()).Get<PublishConfiguration<BasicProperties>>();

                _queueConfiguration = queueConfiguration;
                _publishConfiguration = publishConfiguration ?? new PublishConfiguration<BasicProperties>();

                if (_rabbitMqConnection.GetChannel() is not null &&
                    _rabbitMqConnection.GetChannel().IsOpen) return;

                await _rabbitMqConnection.InitializeAsync(token);
                await _rabbitMqConnection.ConfigureAsync(queueConfiguration, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inicializar produtor do RabbitMQ.");
            }
        }

        public async Task Enqueue(T message, CancellationToken token)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                await _rabbitMqConnection
                    .GetChannel()
                    .BasicPublishAsync(
                        exchange: _queueConfiguration.ExchangeName,
                        routingKey: _queueConfiguration.RoutingKey,
                        mandatory: _publishConfiguration.Mandatary,
                        basicProperties: _publishConfiguration.Properties ?? new BasicProperties(),
                        body: body,
                        cancellationToken: token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao publicar mensagem na exchange.");
            }
        }
    }
}
