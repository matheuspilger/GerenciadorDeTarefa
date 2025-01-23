using GerenciadorDeTarefa.Domain.Entities.Base;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories.Base;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Contexts.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace GerenciadorDeTarefa.Infrastructure.MongoDb.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly IMongoCollection<TEntity> _collection;

        public RepositoryBase(IMongoDbContext mongoDbContext)
        {
            _collection = mongoDbContext.GetCollection<TEntity>();
        }

        public async Task Add(TEntity entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task AddOrUpdateBy(TEntity entity, Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken)
        {
            var options = new FindOneAndReplaceOptions<TEntity>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true
            };

            var result = await _collection.FindOneAndReplaceAsync(filterBy, entity, options, cancellationToken: cancellationToken);
            entity.SetId(result.Id);
        }

        public async Task DeleteBy(Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(filterBy, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> FindManyBy(Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken)
        {
            return await _collection.Find(filterBy).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> FindOneBy(Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken)
        {
            return await _collection.Find(filterBy).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
