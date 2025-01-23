using GerenciadorDeTarefa.Domain.Entities.Base;
using GerenciadorDeTarefa.Domain.Entities.Tarefas;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;

namespace GerenciadorDeTarefa.Domain.Entities.Projetos
{
    public partial class Projeto : EntityBase
    {
        public string Nome { get; private set; }
        public string Sobre { get; private set; }
        public Usuario Usuario { get; private set; }
        public List<Tarefa> Tarefas { get; private set; }
    }
}
