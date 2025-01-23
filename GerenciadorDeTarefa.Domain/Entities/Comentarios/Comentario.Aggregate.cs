using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Entities.Comentarios
{
    public partial class Comentario
    {
        public Comentario(ObjectId id, DateTime dataHora, string descricao)
        {
            Id = id;
            DataHora = dataHora;
            Descricao = descricao;
        }
    }
}
