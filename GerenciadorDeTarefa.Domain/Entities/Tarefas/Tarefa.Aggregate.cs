using GerenciadorDeTarefa.Domain.Constants;
using GerenciadorDeTarefa.Domain.Entities.Comentarios;
using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using GerenciadorDeTarefa.Domain.Enums.Tarefas;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.Domain.Entities.Tarefas
{
    public partial class Tarefa
    {
        public Tarefa(
            ObjectId id,
            string titulo,
            string descricao,
            DateTime vencimento,
            Status status,
            Prioridade prioridade,
            Usuario usuario,
            List<Comentario> comentarios)
        {
            Initialize(id, titulo, descricao, vencimento, status, prioridade, usuario, comentarios);
        }

        public void Update(
            ObjectId id,
            string titulo,
            string descricao,
            DateTime vencimento,
            Status status,
            Prioridade prioridade,
            Usuario usuario,
            List<Comentario> comentarios)
        {
            if (Prioridade != prioridade)
                throw new ArgumentException(ValidationError.AtualizarPrioridadeTarefa);
            Initialize(id, titulo, descricao, vencimento, status, prioridade, usuario, comentarios);
        }

        private void Initialize(ObjectId id,
            string titulo,
            string descricao,
            DateTime vencimento,
            Status status,
            Prioridade prioridade,
            Usuario usuario,
            List<Comentario> comentarios)
        {
            SetId(id);
            Titulo = titulo;
            Descricao = descricao;
            Vencimento = vencimento;
            Prioridade = prioridade;
            Usuario = usuario;
            Comentarios = comentarios;

            SetStatus(status);
        }

        private void SetStatus(Status status)
        {
            Status = status;
            if (status is Status.EmAndamento)
                DataInicio = DateTime.Now;
            else if(status is Status.Concluida)
                DataFim = DateTime.Now;
        }
    }
}
