using GerenciadorDeTarefa.Application.Services.Interfaces;
using GerenciadorDeTarefa.Domain.Entities.Historicos;
using GerenciadorDeTarefa.Domain.Events.Tarefas;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories;

namespace GerenciadorDeTarefa.Application.Services
{
    public class HistoricoTarefaService(
        IUsuarioRepository usuarioRepository,
        IHistoricoTarefaRepository historicoTarefaRepository) : IHistoricoTarefaService
    {
        public async Task Process(EventTarefa evento, CancellationToken cancellationToken)
        {
            var usuario = await usuarioRepository.FindOneBy(x => x.Id == evento.IdUsuario, cancellationToken);
            var historicoTarefa = HistoricoTarefa.Builder.Build(evento.IdProjeto, evento.DataHora, evento.NomeProjeto, usuario, evento.HistoricoTarefas);
            await historicoTarefaRepository.Add(historicoTarefa, cancellationToken);
        }
    }
}
