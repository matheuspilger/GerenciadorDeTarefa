using GerenciadorDeTarefa.Domain.Entities.Base;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Entities.Historicos
{
    public partial class HistoricoTarefa : EntityBase
    {
        public DateTime DataModificacao { get; private set; }
        public ObjectId IdProjeto { get; set; }
        public string NomeProjeto { get; set; }
        public Usuario Usuario { get; private set; }
        public ObjectId IdTarefa { get; private set; }
        public List<HistoricoModificacao> Modificacoes { get; private set; }
    }
}