using GerenciadorDeTarefa.Domain.Entities.Projetos;
using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts.Interfaces;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Repositories.Base;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GerenciadorDeTarefa.Infrastructure.MongoDb.Repositories
{
    public class ProjetoRepository(IMongoDbContext mongoDbContext) : RepositoryBase<Projeto>(mongoDbContext), IProjetoRepository
    {
        public async Task<List<Tarefa>> FilterTarefasBy(Func<Tarefa, bool> filterBy)
        {
            var query = _collection.AsQueryable();
            return await Task.FromResult(query.SelectMany(x => x.Tarefas).Where(filterBy).Select(x => x).ToList());
        }
    }
}