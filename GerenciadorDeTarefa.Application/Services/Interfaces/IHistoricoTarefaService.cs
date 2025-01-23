using GerenciadorDeTarefa.Domain.Events.Tarefas;

namespace GerenciadorDeTarefa.Application.Services.Interfaces
{
    public interface IHistoricoTarefaService
    {
        Task Process(EventTarefa eventTarefa, CancellationToken cancellationToken);
    }
}
