using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Entities.Historicos
{
    public partial class HistoricoTarefa
    {
        public static class Builder
        {
            public static HistoricoTarefa Build(ObjectId idProjeto, DateTime dataModificacao, string nomeProjeto, Usuario usuario, List<Tarefa> historicoTarefas)
                => new(idProjeto, dataModificacao, nomeProjeto, usuario, historicoTarefas);
        }
    }
}
