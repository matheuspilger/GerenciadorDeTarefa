using GerenciadorDeTarefa.Domain.Events.Base;
using GerenciadorDeTarefa.Domain.Interfaces.Bus;
using GerenciadorDeTarefa.Domain.Interfaces.Events;

namespace GerenciadorDeTarefa.Infrastructure.RabbitMq.Bus
{
    public class EventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly Dictionary<string, List<Type>> _handlers = [];

        public void AddSubscription<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName =typeof(TEvent).Name;
            DoAddSubscription(typeof(TEventHandler), eventName);
        }

        private void DoAddSubscription(Type handlerType, string eventName)
        {
            if (!_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, []);

            if (_handlers[eventName].Any(type => type == handlerType))
                throw new ArgumentException(
                    $"O Tipo do Handler {handlerType.Name} já está registrado para o evento '{eventName}'", nameof(handlerType));

            _handlers[eventName].Add(handlerType);
        }

        public void RemoveSubscription<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventName = typeof(TEvent).Name;
            _handlers.TryGetValue(eventName, out var types);

            var handlerType = types!.FirstOrDefault(type => type == typeof(TEventHandler));
            if(handlerType is null)
                throw new ArgumentException(
                    $"O Tipo do Handler {typeof(TEventHandler).Name} não está registrado para o evento '{eventName}'", nameof(handlerType));

            DoRemoveSubscription(eventName, handlerType);
        }

        private void DoRemoveSubscription(string eventName, Type handlerType)
        {
            if (handlerType is null) return;
            _handlers[eventName].Remove(handlerType);

            if (_handlers[eventName].Count is not 0) return;
            _handlers.Remove(eventName);
        }

        public List<Type> GetHandlersForEvent<TEvent>()
        {
            _handlers.TryGetValue(typeof(TEvent).Name, out var types);
            return types ?? [];
        }
    }
}
