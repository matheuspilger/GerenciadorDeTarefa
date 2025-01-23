using GerenciadorDeTarefa.Application.Handlers;
using GerenciadorDeTarefa.Application.Services;
using GerenciadorDeTarefa.Application.Services.Interfaces;
using GerenciadorDeTarefa.Domain.Configurations;
using GerenciadorDeTarefa.Domain.Constants;
using GerenciadorDeTarefa.Domain.Events.Tarefas;
using GerenciadorDeTarefa.Domain.Interfaces.Bus;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts.Interfaces;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Loads;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Loads.Interfaces;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Repositories;
using GerenciadorDeTarefa.Infrastructure.RabbitMq.Bus;
using GerenciadorDeTarefa.Infrastructure.RabbitMq.Connection;
using GerenciadorDeTarefa.Infrastructure.RabbitMq.Consumer;
using GerenciadorDeTarefa.Infrastructure.RabbitMq.Interfaces;
using GerenciadorDeTarefa.Infrastructure.RabbitMq.Publish;

namespace GerenciadorDeTarefa.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
            => services
                .RegisterConfigs(configuration)
                .RegisterMediatR()
                .RegisterMongoDb()
                .RegisterRabbitMq()
                .RegisterServices()
                .RegisterRepositories()
                .RegisterDatabaseLoaders()
                .RegisterEventBus()
                .RegisterEventBusHandling();

        private static IServiceCollection RegisterConfigs(this IServiceCollection services, IConfiguration configuration)
            => services
                .Configure<DatabaseConfiguration>(configuration.GetSection(OptionsPath.Database))
                .Configure<RabbitMqConfiguration>(configuration.GetSection(OptionsPath.RabbitMq))
                .Configure<QueueConfiguration>(configuration.GetSection(OptionsPath.QueueHistoricoTarefa))
                .Configure<ReportConfiguration>(configuration.GetSection(OptionsPath.Report));

        private static IServiceCollection RegisterMediatR(this IServiceCollection services)
            => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProjetoHandler).Assembly));

        private static IServiceCollection RegisterMongoDb(this IServiceCollection services)
            => services.AddSingleton<IMongoDbContext, MongoDbContext>();

        private static IServiceCollection RegisterRabbitMq(this IServiceCollection services)
            => services
                .AddSingleton<IRabbitMqConnection, RabbitMqConnection>()
                .AddSingleton<IPublishProvider<EventTarefa>, PublishProvider<EventTarefa>>()
                .AddSingleton<IConsumerProvider<EventTarefa>, ConsumerProvider<EventTarefa>>();

        private static IServiceCollection RegisterServices(this IServiceCollection services)
            => services
                .AddScoped<IProjetoService, ProjetoService>()
                .AddScoped<ITarefaService, TarefaService>()
                .AddScoped<IHistoricoTarefaService, HistoricoTarefaService>()
                .AddScoped<IDesempenhoService, DesempenhoService>();

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
            => services
                .AddScoped<IProjetoRepository, ProjetoRepository>()
                .AddScoped<IHistoricoTarefaRepository, HistoricoTarefaRepository>()
                .AddScoped<IUsuarioRepository, UsuarioRepository>();

        private static IServiceCollection RegisterDatabaseLoaders(this IServiceCollection services)
            => services
                .AddScoped<IUsuarioDefaultLoader, UsuarioDefaultLoader>();

        private static IServiceCollection RegisterEventBus(this IServiceCollection services)
            => services
                .AddSingleton<IEventBus, RabbitMqEventBus>()
                .AddSingleton<IEventBusSubscriptionManager, EventBusSubscriptionManager>();

        private static IServiceCollection RegisterEventBusHandling(this IServiceCollection services)
            => services.AddScoped<EventTarefaHandler>();
    }
}
