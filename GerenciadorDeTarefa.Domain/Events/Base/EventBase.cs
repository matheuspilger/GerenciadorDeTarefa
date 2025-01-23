using GerenciadorDeTarefa.Domain.Helpers.Converters;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace GerenciadorDeTarefa.Domain.Events.Base
{
    public class EventBase(ObjectId idUsuario)
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId IdUsuario { get; set; } = idUsuario;
        public DateTime DataHora { get; set; } = DateTime.Now;
    }
}
