namespace GerenciadorDeTarefa.Domain.Constants
{
    public class ValidationError
    {
        public const string DeletarProjeto = "Não é possivel deletar um projeto com tarefas pendentes, conclua ou remova as tarefas pendentes";
        public const string AtualizarPrioridadeTarefa = "Não é possivel alterar a prioridade de uma tarefa.";
        public const string NumeroMaximoTarefa = "O número máximo de tarefas(20) para esse projeto foi atingido.";
    }
}
