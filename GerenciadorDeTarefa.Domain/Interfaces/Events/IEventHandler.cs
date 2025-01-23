using GerenciadorDeTarefa.Domain.Events.Base;

namespace GerenciadorDeTarefa.Domain.Interfaces.Events
{
    public interface IEventHandler<in TEvent>
        where TEvent : EventBase
    {
        Task Handle(TEvent evento, CancellationToken cancellationToken);
    }
}
