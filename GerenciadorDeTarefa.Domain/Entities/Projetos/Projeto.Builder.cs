using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using GerenciadorDeTarefa.Domain.Helpers;

namespace GerenciadorDeTarefa.Domain.Entities.Projetos
{
    public partial class Projeto
    {
        public static class Builder
        {
            public static Projeto Build(
                string id,
                string nome,
                string sobre,
                Usuario usuario)
                    => new(id.ToObjectId(), nome, sobre, usuario);
        }
    }
}
