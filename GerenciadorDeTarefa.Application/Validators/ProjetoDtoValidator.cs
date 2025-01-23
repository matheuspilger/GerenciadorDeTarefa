using FluentValidation;
using GerenciadorDeTarefa.Application.Dtos.Projetos;

namespace GerenciadorDeTarefa.Application.Validators
{
    public class ProjetoDtoValidator : AbstractValidator<ProjetoDto>
    {
        public ProjetoDtoValidator()
        {
            RuleFor(x => x.Nome).MinimumLength(2);
            RuleFor(x => x.Sobre).MinimumLength(2);
            RuleFor(x => x.IdUsuario).NotEmpty();
        }
    }
}
