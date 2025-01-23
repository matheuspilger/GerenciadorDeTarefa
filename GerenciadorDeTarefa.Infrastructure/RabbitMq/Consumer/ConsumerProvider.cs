using GerenciadorDeTarefa.Domain.Configurations;
using GerenciadorDeTarefa.Domain.Events.Base;
using GerenciadorDeTarefa.Domain.Helpers;
using GerenciadorDeTarefa.Infrastructure.RabbitMq.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GerenciadorDeTarefa.Infrastructure.RabbitMq.Consumer
{
    public class ConsumerProvider<T> : IConsumerProvider<T>
        where T : EventBase
    {
        private QueueConfiguration _queueConfiguration = default!;
        private ConsumerConfiguration _consumerConfiguration = default!;
        private readonly IConfiguration _configuration;
        private readonly IRabbitMqConnection _rabbitMqConnection;
        private readonly ILogger<ConsumerProvider<T>> _logger;

        public event EventHandler<T> OnMessageReceived = default!;

        public ConsumerProvider(
            IConfiguration configuration,
            IRabbitMqConnection rabbitMqConnection,
            ILogger<ConsumerProvider<T>> logger)
        {
            _configuration = configuration;
            _rabbitMqConnection = rabbitMqConnection;
            _logger = logger;
        }

        public async Task InitializeAsync(CancellationToken token)
        {
            try
            {
                _queueConfiguration = _configuration.GetSection(typeof(T).GetQueueConfig()).Get<QueueConfiguration>()
                    ?? throw new Exception($"Configuração da queue {typeof(T).Name} não localizada.");
                _consumerConfiguration = _configuration.GetSection(typeof(T).GetConsumerConfig()).Get<ConsumerConfiguration>()
                    ??  new ConsumerConfiguration();

                if (_rabbitMqConnection.GetChannel() is not null &&
                    _rabbitMqConnection.GetChannel().IsOpen) return;

                await _rabbitMqConnection.InitializeAsync(token);
                await _rabbitMqConnection.ConfigureAsync(_queueConfiguration, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inicializar consumidor do RabbitMQ.");
            }
        }

        public async Task StartConsumerAsync(CancellationToken token)
        {
            var consumer = new AsyncEventingBasicConsumer(_rabbitMqConnection.GetChannel());
            consumer.ReceivedAsync += ConsumerReceived;
            await _rabbitMqConnection
                .GetChannel()
                .BasicConsumeAsync(
                    queue: _queueConfiguration.QueueName,
                    autoAck: _consumerConfiguration.AutoAck,
                    consumerTag: _consumerConfiguration.ConsumerTag,
                    noLocal: _consumerConfiguration.NoLocal,
                    exclusive: _consumerConfiguration.Exclusive,
                    arguments: _consumerConfiguration.Arguments,
                    consumer: consumer,
                    cancellationToken: token);
        }

        private async Task ConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var retorno = Encoding.UTF8.GetString(body);
            if (retorno is null) return;
            OnMessageReceived?.Invoke(this, JsonSerializer.Deserialize<T>(retorno)!);
            await _rabbitMqConnection.GetChannel().BasicAckAsync(deliveryTag: e.DeliveryTag, multiple: false);
        }
    }
}