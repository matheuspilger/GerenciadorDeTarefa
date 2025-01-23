using FluentValidation;
using GerenciadorDeTarefa.Application.Dtos.Tarefas;

namespace GerenciadorDeTarefa.Application.Validators
{
    public class TarefaDeleteDtoValidator : AbstractValidator<TarefaDeleteDto>
    {
        public TarefaDeleteDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.IdProjeto).NotEmpty();
        }
    }
}
