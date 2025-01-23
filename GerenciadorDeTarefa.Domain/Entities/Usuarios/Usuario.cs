using GerenciadorDeTarefa.Domain.Entities.Base;
using GerenciadorDeTarefa.Domain.Enums.Usuarios;

namespace GerenciadorDeTarefa.Domain.Entities.Usuarios
{
    public partial class Usuario : EntityBase
    {
        public bool Ativo { get; private set; }
        public string NomeCompleto { get; private set; }
        public string Acesso { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public Funcao Funcao { get; private set; }
    }
}
