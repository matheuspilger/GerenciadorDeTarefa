using GerenciadorDeTarefa.Domain.Configurations;
using GerenciadorDeTarefa.Domain.Entities.Base;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Constants;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts
{
    public class MongoDbContext(IOptions<DatabaseConfiguration> config) : IMongoDbContext
    {
        private readonly DatabaseConfiguration _databaseConfig = config.Value;
        private IMongoDatabase _database = default!;

        public void Initialize()
        {
            var client = new MongoClient(_databaseConfig.ConnectionString);
            _database = client.GetDatabase(_databaseConfig.DatabaseName);

            var pack = new ConventionPack
            {
                new IgnoreIfNullConvention(true)
            };

            ConventionRegistry.Register(Ignore.NullProperty, pack, filter => filter.Assembly == typeof(EntityBase).Assembly);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : EntityBase
            => _database.GetCollection<TEntity>(typeof(TEntity).Name);
    }
}
