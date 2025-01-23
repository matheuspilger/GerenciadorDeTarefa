using GerenciadorDeTarefa.Domain.Events.Base;
using GerenciadorDeTarefa.Domain.Interfaces.Events;

namespace GerenciadorDeTarefa.Domain.Interfaces.Bus
{
    public interface IEventBusSubscriptionManager
    {
        void AddSubscription<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>;
        void RemoveSubscription<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>;

        List<Type> GetHandlersForEvent<TEvent>();
    }
}
