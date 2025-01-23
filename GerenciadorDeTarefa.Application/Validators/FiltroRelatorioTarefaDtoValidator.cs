using FluentValidation;
using GerenciadorDeTarefa.Application.Dtos.Relatorios;

namespace GerenciadorDeTarefa.Application.Validators
{
    public class FiltroRelatorioTarefaDtoValidator : AbstractValidator<FiltroRelatorioTarefaDto>
    {
        public FiltroRelatorioTarefaDtoValidator()
        {
            RuleFor(x => x.IdUsuarioGerente).NotNull().NotEmpty();
        }
    }
}
