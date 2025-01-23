using GerenciadorDeTarefa.Application.Services.Interfaces;
using GerenciadorDeTarefa.Domain.Events.Tarefas;
using GerenciadorDeTarefa.Domain.Interfaces.Events;

namespace GerenciadorDeTarefa.Application.Handlers
{
    public class EventTarefaHandler(IHistoricoTarefaService historicoTarefaService) : IEventHandler<EventTarefa>
    {
        public async Task Handle(EventTarefa evento, CancellationToken cancellationToken)
        {
            await historicoTarefaService.Process(evento, cancellationToken);
        }
    }
}
