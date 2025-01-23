using GerenciadorDeTarefa.Domain.Entities.Comentarios;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using GerenciadorDeTarefa.Domain.Enums.Tarefas;
using GerenciadorDeTarefa.Domain.Helpers;

namespace GerenciadorDeTarefa.Domain.Entities.Tarefas
{
    public partial class Tarefa
    {
        public static class Builder
        {
            public static Tarefa Build(string id, string titulo, string descricao, DateTime vencimento, Status status, Prioridade prioridade, Usuario usuario, List<Comentario> comentarios)
                => new(id.ToObjectId(), titulo, descricao, vencimento, status, prioridade, usuario, comentarios);
        }
    }
}
