using FluentValidation;
using GerenciadorDeTarefa.Application.Dtos.Tarefas;

namespace GerenciadorDeTarefa.Application.Validators
{
    public class TarefaDtoValidator : AbstractValidator<TarefaDto>
    {
        public TarefaDtoValidator()
        {
            RuleFor(x => x.IdProjeto).NotEmpty();
            RuleFor(x => x.Titulo).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Descricao).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Vencimento).GreaterThan(DateTime.MinValue);
            RuleFor(x => x.IdUsuario).NotEmpty();
            RuleFor(x => x.IdUsuarioModificacao).NotEmpty();
        }
    }
}
