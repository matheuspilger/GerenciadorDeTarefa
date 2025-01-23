using GerenciadorDeTarefa.Domain.Helpers.Converters;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace GerenciadorDeTarefa.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        [BsonIgnoreIfDefault]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; private set; }

        public void SetId(ObjectId id) => Id = id;
    }
}
