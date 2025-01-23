using GerenciadorDeTarefa.Domain.Configurations;
using RabbitMQ.Client;

namespace GerenciadorDeTarefa.Infrastructure.RabbitMq.Interfaces
{
    public interface IRabbitMqConnection
    {
        Task InitializeAsync(CancellationToken token);
        Task ConfigureAsync(QueueConfiguration queue, CancellationToken token);
        IChannel GetChannel();
    }
}
