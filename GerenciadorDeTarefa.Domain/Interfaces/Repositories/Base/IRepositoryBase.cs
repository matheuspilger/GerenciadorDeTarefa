using GerenciadorDeTarefa.Domain.Entities.Base;
using System.Linq.Expressions;

namespace GerenciadorDeTarefa.Domain.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task Add(TEntity entity, CancellationToken cancellationToken);
        Task AddOrUpdateBy(TEntity entity, Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken);
        Task DeleteBy(Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken);
        Task<TEntity> FindOneBy(Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> FindManyBy(Expression<Func<TEntity, bool>> filterBy, CancellationToken cancellationToken);
    }
}
