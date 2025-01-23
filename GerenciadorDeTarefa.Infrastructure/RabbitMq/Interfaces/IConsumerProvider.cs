using GerenciadorDeTarefa.Domain.Configurations;
using GerenciadorDeTarefa.Domain.Events.Base;

namespace GerenciadorDeTarefa.Infrastructure.RabbitMq.Interfaces
{
    public interface IConsumerProvider<T> where T : EventBase
    {
        event EventHandler<T> OnMessageReceived;
        Task InitializeAsync(CancellationToken token);
        Task StartConsumerAsync(CancellationToken token);
    }
}