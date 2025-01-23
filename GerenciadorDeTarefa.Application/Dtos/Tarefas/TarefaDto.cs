using GerenciadorDeTarefa.Domain.Enums.Tarefas;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.Application.Dtos.Tarefas
{
    public class TarefaDto
    {
        public string Id { get; set; }
        public string IdProjeto { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Vencimento { get; set; }
        public Status Status { get; set; }
        public Prioridade Prioridade { get; set; }
        public List<ComentarioDto> Comentarios { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioModificacao { get; set; }
    }
}
