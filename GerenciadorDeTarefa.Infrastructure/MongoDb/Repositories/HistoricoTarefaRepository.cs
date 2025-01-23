using GerenciadorDeTarefa.Domain.Entities.Historicos;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts.Interfaces;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Repositories.Base;

namespace GerenciadorDeTarefa.Infrastructure.MongoDb.Repositories
{
    public class HistoricoTarefaRepository(IMongoDbContext mongoDbContext) : RepositoryBase<HistoricoTarefa>(mongoDbContext), IHistoricoTarefaRepository
    {
    }
}
