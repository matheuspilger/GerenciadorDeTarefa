using FluentValidation;
using GerenciadorDeTarefa.Application.Dtos.Projetos;

namespace GerenciadorDeTarefa.Application.Validators
{
    public class ProjetoGetDtoValidator : AbstractValidator<ProjetoGetDto>
    {
        public ProjetoGetDtoValidator()
        {
            RuleFor(x => x.IdUsuario).NotEmpty();
        }
    }
}
