using GerenciadorDeTarefa.Domain.Events.Base;
using GerenciadorDeTarefa.Domain.Interfaces.Events;

namespace GerenciadorDeTarefa.Domain.Interfaces.Bus
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent evento, CancellationToken token)
            where TEvent : EventBase;

        Task Subscribe<TEvent, TEventHandler>(CancellationToken token)
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>;

        void Unsubscribe<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>;
    }
}
