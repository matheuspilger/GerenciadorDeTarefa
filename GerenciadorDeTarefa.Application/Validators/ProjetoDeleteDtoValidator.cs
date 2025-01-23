using FluentValidation;
using GerenciadorDeTarefa.Application.Dtos.Projetos;

namespace GerenciadorDeTarefa.Application.Validators
{
    public class ProjetoDeleteDtoValidator : AbstractValidator<ProjetoDeleteDto>
    {
        public ProjetoDeleteDtoValidator()
        {
            RuleFor(x => x.IdProjeto).NotEmpty();
        }
    }
}
