using GerenciadorDeTarefa.Domain.Constants;
using GerenciadorDeTarefa.Domain.Entities.Comentarios;
using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using GerenciadorDeTarefa.Domain.Enums.Tarefas;
using GerenciadorDeTarefa.Domain.Helpers;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Entities.Projetos
{
    public partial class Projeto
    {
        public Projeto(
            ObjectId id,
            string nome,
            string sobre,
            Usuario usuario)
        {
            SetId(id);
            Nome = nome;
            Sobre = sobre;
            Usuario = usuario;
        }

        public (ObjectId Id, List<Tarefa> HistoricoTarefas) AddOrUpdateTarefa(
            string id,
            string titulo,
            string descricao,
            DateTime vencimento,
            Status status,
            Prioridade prioridade,
            Usuario usuario,
            List<Comentario> comentarios)
        {
            Tarefas ??= [];
            var historicoTarefas = new List<Tarefa>();
            var tarefa = Tarefas.FirstOrDefault(t => t.Id == id.ToObjectId());

            if (tarefa is null)
            {
                if (Tarefas.Count is 20)
                    throw new ArgumentException(ValidationError.NumeroMaximoTarefa);

                tarefa = Tarefa.Builder.Build(string.Empty, titulo, descricao, vencimento, status, prioridade, usuario, comentarios);
                Tarefas.Add(tarefa);
            }
            else
            {
                var tarefaAntiga = tarefa.Clone();
                tarefa.Update(id.ToObjectId(), titulo, descricao, vencimento, status, prioridade, usuario, comentarios);

                historicoTarefas.Add((Tarefa) tarefaAntiga);
                historicoTarefas.Add(tarefa);
            }
            return (tarefa.Id, historicoTarefas);
        }

        public void RemoverTarefa(string id)
            => Tarefas.Remove(Tarefas.FirstOrDefault(t => t.Id == id.ToObjectId()));

        public bool IsDeletable()
            => Tarefas.Any(t => t.Status == Status.Pendente)  ? throw new ArgumentException(ValidationError.DeletarProjeto) : true;
    }
}
