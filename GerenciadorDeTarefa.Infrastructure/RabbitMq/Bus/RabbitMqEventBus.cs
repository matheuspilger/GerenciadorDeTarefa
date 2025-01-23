using GerenciadorDeTarefa.Domain.Events.Base;
using GerenciadorDeTarefa.Domain.Interfaces.Bus;
using GerenciadorDeTarefa.Domain.Interfaces.Events;
using GerenciadorDeTarefa.Infrastructure.RabbitMq.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDeTarefa.Infrastructure.RabbitMq.Bus
{
    public class RabbitMqEventBus(
        IServiceProvider serviceProvider,
        IEventBusSubscriptionManager eventBusSubscriptionManager) : IEventBus
    {
        public async Task Publish<TEvent>(TEvent evento, CancellationToken token)
            where TEvent : EventBase
        {
            var publishProvider = serviceProvider.GetService<IPublishProvider<TEvent>>()
                ?? throw new Exception($"Publish {typeof(TEvent).Name} não localizado.");

            await publishProvider.InitializeAsync(token);
            await publishProvider.Enqueue(evento, token);
        }

        public async Task Subscribe<TEvent, TEventHandler>(CancellationToken token)
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>
        {
            eventBusSubscriptionManager.AddSubscription<TEvent, TEventHandler>();

            var consumerProvider = serviceProvider.GetService<IConsumerProvider<TEvent>>()
                ?? throw new Exception($"Consumer {typeof(TEvent).Name} não localizado.");
            await consumerProvider.InitializeAsync(token);
            await consumerProvider.StartConsumerAsync(token);
            consumerProvider.OnMessageReceived +=
                async (sender, eventArgs) => await ProcessMessage(eventArgs, token);
        }

        public void Unsubscribe<TEvent, TEventHandler>()
            where TEvent : EventBase
            where TEventHandler : IEventHandler<TEvent>
        {
            eventBusSubscriptionManager.RemoveSubscription<TEvent, TEventHandler>();
        }

        private async Task ProcessMessage<TEvent>(TEvent evento, CancellationToken token) where TEvent : EventBase
        {
            var types = eventBusSubscriptionManager.GetHandlersForEvent<TEvent>();
            foreach (var type in types)
            {
                using var scope = serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetService(type);
                if (handler is null) continue;

                var concreteType = typeof(IEventHandler<>).MakeGenericType(typeof(TEvent));
                await Task.Yield();
                await (Task)concreteType.GetMethod(nameof(IEventHandler<TEvent>.Handle))!.Invoke(handler, [evento, token])!;
            }
        }
    }
}