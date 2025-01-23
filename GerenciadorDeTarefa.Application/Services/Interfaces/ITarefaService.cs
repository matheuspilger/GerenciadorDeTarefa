using GerenciadorDeTarefa.Application.Dtos.Tarefas;

namespace GerenciadorDeTarefa.Application.Services.Interfaces
{
    public interface ITarefaService
    {
        Task<string> AddOrUpdate(TarefaDto tarefa, CancellationToken token);
        Task<string> Delete(TarefaDeleteDto tarefa, CancellationToken token);
        Task<List<TarefaDto>> GetMany(TarefaGetDto tarefa, CancellationToken cancellationToken);
    }
}
