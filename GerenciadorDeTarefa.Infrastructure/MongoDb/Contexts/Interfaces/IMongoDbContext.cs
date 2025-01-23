using GerenciadorDeTarefa.Domain.Entities.Base;
using MongoDB.Driver;

namespace GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts.Interfaces
{
    public interface IMongoDbContext
    {
        void Initialize();
        IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : EntityBase;
    }
}
