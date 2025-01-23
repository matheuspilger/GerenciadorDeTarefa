using GerenciadorDeTarefa.Domain.Enums.Usuarios;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Entities.Usuarios
{
    public partial class Usuario
    {
        public Usuario(ObjectId id, bool ativo, string nomeCompleto, string acesso, string email, string senha, Funcao funcao)
        {
            SetId(id);
            Ativo = ativo;
            NomeCompleto = nomeCompleto;
            Acesso = acesso;
            Email = email;
            Senha = senha;
            Funcao = funcao;
        }
    }
}
