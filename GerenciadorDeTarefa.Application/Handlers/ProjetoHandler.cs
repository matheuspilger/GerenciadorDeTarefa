using GerenciadorDeTarefa.Application.Dtos.Projetos;
using GerenciadorDeTarefa.Application.Services.Interfaces;
using GerenciadorDeTarefa.Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefa.Application.Handlers
{
    public record AddOrUpdateProjetoCommand(ProjetoDto Dto) : IRequest<IActionResult>;
    public record DeleteProjetoCommand(ProjetoDeleteDto Dto) : IRequest<IActionResult>;
    public record GetProjetoCommand(ProjetoGetDto Dto) : IRequest<IActionResult>;

    public class ProjetoHandler(IProjetoService projetoService) :
        IRequestHandler<AddOrUpdateProjetoCommand, IActionResult>,
        IRequestHandler<DeleteProjetoCommand, IActionResult>,
        IRequestHandler<GetProjetoCommand, IActionResult>
    {
        public async Task<IActionResult> Handle(AddOrUpdateProjetoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projetoValidator = new ProjetoDtoValidator();
                var result = await projetoValidator.ValidateAsync(request.Dto, cancellationToken);

                if (result.IsValid)
                {
                    var idProjeto = await projetoService.AddOrUpdate(request.Dto, cancellationToken);
                    return new OkObjectResult(idProjeto);
                }
                else
                {
                    return new BadRequestObjectResult(result.Errors.Select(x => x.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> Handle(DeleteProjetoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projetoValidator = new ProjetoDeleteDtoValidator();
                var result = await projetoValidator.ValidateAsync(request.Dto, cancellationToken);

                if (result.IsValid)
                {
                    var idProjeto = await projetoService.Delete(request.Dto, cancellationToken);
                    return new OkObjectResult(idProjeto);
                }
                else
                {
                    return new BadRequestObjectResult(result.Errors.Select(x => x.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> Handle(GetProjetoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projetoValidator = new ProjetoGetDtoValidator();
                var result = await projetoValidator.ValidateAsync(request.Dto, cancellationToken);

                if (result.IsValid)
                {
                    var projetos = await projetoService.GetMany(request.Dto, cancellationToken);
                    return new OkObjectResult(projetos);
                }
                else
                {
                    return new BadRequestObjectResult(result.Errors.Select(x => x.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
