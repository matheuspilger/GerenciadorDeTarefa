using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Entities.Comentarios
{
    public partial class Comentario
    {
        public static class Builder
        {
            public static Comentario Build(ObjectId id, DateTime dataHora, string descricao)
                => new(id, dataHora, descricao);
        }
    }
}
