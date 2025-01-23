using GerenciadorDeTarefa.Domain.Enums.Tarefas;

namespace GerenciadorDeTarefa.Application.Dtos.Relatorios
{
    public class FiltroRelatorioTarefaDto
    {
        public Status? Status { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string IdUsuario { get; set; }
        public string IdUsuarioGerente { get; set; }
    }
}
