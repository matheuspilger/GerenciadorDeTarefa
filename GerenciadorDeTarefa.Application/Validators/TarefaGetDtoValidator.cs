using FluentValidation;
using GerenciadorDeTarefa.Application.Dtos.Tarefas;

namespace GerenciadorDeTarefa.Application.Validators
{
    public class TarefaGetDtoValidator : AbstractValidator<TarefaGetDto>
    {
        public TarefaGetDtoValidator()
        {
            RuleFor(x => x.IdProjeto).NotEmpty();
        }
    }
}