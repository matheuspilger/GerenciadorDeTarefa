using GerenciadorDeTarefa.Domain.Entities.Projetos;
using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories.Base;
using System.Linq.Expressions;

namespace GerenciadorDeTarefa.Domain.Interfaces.Repositories
{
    public interface IProjetoRepository : IRepositoryBase<Projeto>
    {
        Task<List<Tarefa>> FilterTarefasBy(Func<Tarefa, bool> filterBy);
    }
}
