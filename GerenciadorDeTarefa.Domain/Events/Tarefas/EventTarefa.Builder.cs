using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Helpers;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Events.Tarefas
{
    public partial class EventTarefa
    {
        public static class Builder
        {
            public static EventTarefa Build(string idUsuario, ObjectId idProjeto, string nomeProjeto, List<Tarefa> historicoTarefas)
                => new(idUsuario.ToObjectId(), idProjeto, nomeProjeto, historicoTarefas);
        }
    }
}
