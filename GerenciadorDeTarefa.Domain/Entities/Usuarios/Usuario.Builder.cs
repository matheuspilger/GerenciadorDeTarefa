using GerenciadorDeTarefa.Domain.Enums.Usuarios;
using GerenciadorDeTarefa.Domain.Helpers;

namespace GerenciadorDeTarefa.Domain.Entities.Usuarios
{
    public partial class Usuario
    {
        public static class Builder
        {
            public static Usuario Build(string id, bool ativo, string nomeCompleto, string acesso, string email, string senha, Funcao funcao)
                => new (id.ToObjectId(), ativo, nomeCompleto, acesso, email, senha, funcao);
        }
    }
}
