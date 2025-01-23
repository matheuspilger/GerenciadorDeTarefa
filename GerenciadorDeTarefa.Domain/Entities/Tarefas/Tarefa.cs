using GerenciadorDeTarefa.Domain.Entities.Base;
using GerenciadorDeTarefa.Domain.Entities.Comentarios;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using GerenciadorDeTarefa.Domain.Enums.Tarefas;

namespace GerenciadorDeTarefa.Domain.Entities.Tarefas
{
    public partial class Tarefa : EntityBase, ICloneable
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime Vencimento { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public Status Status { get; private set; }
        public Prioridade Prioridade { get; private set; }
        public Usuario Usuario { get; private set; }
        public List<Comentario> Comentarios { get; private set; }

        public object Clone()
        {
            return (Tarefa)MemberwiseClone();
        }
    }
}
