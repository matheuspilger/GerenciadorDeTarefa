using GerenciadorDeTarefa.Application.Handlers;
using GerenciadorDeTarefa.Domain.Events.Tarefas;
using GerenciadorDeTarefa.Domain.Interfaces.Bus;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts.Interfaces;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Loads.Interfaces;

namespace GerenciadorDeTarefa.Api.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void InitializeDependencies(this WebApplication app, CancellationToken token)
        {
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IMongoDbContext>().Initialize();
                scope.ServiceProvider.GetRequiredService<IUsuarioDefaultLoader>().Load(token);
                scope.ServiceProvider.GetRequiredService<IEventBus>().Subscribe<EventTarefa, EventTarefaHandler>(token);
            }
        }
    }
}