using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Entities.Comentarios
{
    public partial class Comentario
    {
        public ObjectId Id { get; private set; }
        public DateTime DataHora { get; private set; }
        public string Descricao { get; private set; }
    }
}