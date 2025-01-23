using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts.Interfaces;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Repositories.Base;

namespace GerenciadorDeTarefa.Infrastructure.MongoDb.Repositories
{
    public class UsuarioRepository(IMongoDbContext mongoDbContext) : RepositoryBase<Usuario>(mongoDbContext), IUsuarioRepository
    {

    }
}
