using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Helpers
{
    public static class ObjectIdHelper
    {
        public static ObjectId ToObjectId(this string id)
        {
            return !string.IsNullOrWhiteSpace(id) ? ObjectId.Parse(id) : ObjectId.GenerateNewId();
        }
    }
}
