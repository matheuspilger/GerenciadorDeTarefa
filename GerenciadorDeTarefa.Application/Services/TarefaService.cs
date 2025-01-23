using GerenciadorDeTarefa.Application.Dtos.Tarefas;
using GerenciadorDeTarefa.Application.Services.Interfaces;
using GerenciadorDeTarefa.Domain.Entities.Comentarios;
using GerenciadorDeTarefa.Domain.Events.Tarefas;
using GerenciadorDeTarefa.Domain.Helpers;
using GerenciadorDeTarefa.Domain.Interfaces.Bus;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories;

namespace GerenciadorDeTarefa.Application.Services
{
    public class TarefaService(
        IProjetoRepository projetoRepository,
        IUsuarioRepository usuarioRepository,
        IEventBus eventBus) : ITarefaService
    {
        public async Task<string> AddOrUpdate(TarefaDto tarefaDto, CancellationToken token)
        {
            var projeto = await projetoRepository.FindOneBy(p => p.Id == tarefaDto.IdProjeto.ToObjectId(), token);
            var usuario = await usuarioRepository.FindOneBy(p => p.Id == tarefaDto.IdUsuario.ToObjectId(), token);

            var tarefa = projeto.AddOrUpdateTarefa(tarefaDto.Id, tarefaDto.Titulo, tarefaDto.Descricao, tarefaDto.Vencimento, tarefaDto.Status, tarefaDto.Prioridade,
                usuario, tarefaDto.Comentarios.Select(c => Comentario.Builder.Build(c.Id.ToObjectId(), c.DataHora, c.Descricao)).ToList());

            await projetoRepository.AddOrUpdateBy(projeto, x => x.Id == tarefaDto.IdProjeto.ToObjectId(), token);
            
            if (tarefa.HistoricoTarefas.Count == 0) return tarefa.Id.ToString();

            await eventBus.Publish(EventTarefa.Builder.Build(
                tarefaDto.IdUsuarioModificacao, projeto.Id, projeto.Nome, tarefa.HistoricoTarefas), token);

            return tarefa.Id.ToString();
        }

        public async Task<string> Delete(TarefaDeleteDto tarefa, CancellationToken token)
        {
            var projeto = await projetoRepository.FindOneBy(p => p.Id.ToString() == tarefa.IdProjeto, token);
            projeto.RemoverTarefa(tarefa.Id);
            return tarefa.Id;
        }

        public async Task<List<TarefaDto>> GetMany(TarefaGetDto tarefa, CancellationToken cancellationToken)
        {
            var projeto = await projetoRepository.FindOneBy(p => p.Id == tarefa.IdProjeto.ToObjectId(), cancellationToken);
            if(projeto.Tarefas is null) return [];

            return projeto.Tarefas.Select(t => new TarefaDto
            {
                Id = t.Id.ToString(),
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Prioridade = t.Prioridade,
                Status = t.Status,
                Vencimento = t.Vencimento,
                Comentarios = t.Comentarios.Select(c => new ComentarioDto { Id = c.Id.ToString(), DataHora = c.DataHora, Descricao = c.Descricao }).ToList(),
                IdProjeto = tarefa.IdProjeto,
                IdUsuario = t.Usuario.Id.ToString()
            }).ToList();
        }
    }
}
