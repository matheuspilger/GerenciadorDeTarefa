using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Events.Base;
using GerenciadorDeTarefa.Domain.Helpers.Converters;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace GerenciadorDeTarefa.Domain.Events.Tarefas
{
    public partial class EventTarefa(
        ObjectId idUsuario,
        ObjectId idProjeto,
        string nomeProjeto,
        List<Tarefa> historicoTarefas) : EventBase(idUsuario)
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId IdProjeto { get; } = idProjeto;
        public string NomeProjeto { get; } = nomeProjeto;
        public List<Tarefa> HistoricoTarefas { get; set; } = historicoTarefas;
    }
}
