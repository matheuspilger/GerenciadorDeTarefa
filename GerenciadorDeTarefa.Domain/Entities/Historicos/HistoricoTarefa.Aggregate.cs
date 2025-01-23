using AnyDiff.Extensions;
using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Entities.Historicos
{
    public partial class HistoricoTarefa
    {
        public HistoricoTarefa(ObjectId idProjeto, DateTime dataModificacao, string nomeProjeto, Usuario usuario, List<Tarefa> historicoTarefas)
        {
            DataModificacao = dataModificacao;
            IdProjeto = idProjeto;
            NomeProjeto = nomeProjeto;
            Usuario = usuario;
            IdTarefa = historicoTarefas[0].Id;

            SetModificacoes(historicoTarefas);
        }

        private void SetModificacoes(List<Tarefa> historicoTarefas)
        {
            var tarefaAntiga = historicoTarefas[0];
            var tarefaAtual = historicoTarefas[1];

            var diff = tarefaAntiga.Diff(tarefaAtual);
            if (diff.Count is 0) return;
            Modificacoes = diff.Select(d => HistoricoModificacao.Builder.Build(d.Property, d.LeftValue, d.RightValue)).ToList();
        }
    }
}
