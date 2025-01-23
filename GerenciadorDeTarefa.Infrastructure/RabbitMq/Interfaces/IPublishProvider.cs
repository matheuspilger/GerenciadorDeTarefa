using GerenciadorDeTarefa.Domain.Events.Base;

namespace GerenciadorDeTarefa.Infrastructure.RabbitMq.Interfaces
{
    public interface IPublishProvider<T> where T : EventBase
    {
        Task InitializeAsync(CancellationToken token);
        Task Enqueue(T message, CancellationToken token);
    }
}