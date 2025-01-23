using GerenciadorDeTarefa.Application.Dtos.Tarefas;
using GerenciadorDeTarefa.Application.Services.Interfaces;
using GerenciadorDeTarefa.Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefa.Application.Handlers
{
    public record AddOrUpdateTarefaCommand(TarefaDto Dto) : IRequest<IActionResult>;
    public record DeleteTarefaCommand(TarefaDeleteDto Dto) : IRequest<IActionResult>;
    public record GetTarefaCommand(TarefaGetDto Dto) : IRequest<IActionResult>;

    public class TarefaHandler(ITarefaService tarefaService) :
        IRequestHandler<AddOrUpdateTarefaCommand, IActionResult>,
        IRequestHandler<DeleteTarefaCommand, IActionResult>,
        IRequestHandler<GetTarefaCommand, IActionResult>
    {
        public async Task<IActionResult> Handle(AddOrUpdateTarefaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tarefaValidator = new TarefaDtoValidator();
                var result = await tarefaValidator.ValidateAsync(request.Dto, cancellationToken);

                if(result.IsValid)
                {
                    var idTarefa = await tarefaService.AddOrUpdate(request.Dto, cancellationToken);
                    return new OkObjectResult(idTarefa);
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

        public async Task<IActionResult> Handle(DeleteTarefaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tarefaValidator = new TarefaDeleteDtoValidator();
                var result = await tarefaValidator.ValidateAsync(request.Dto, cancellationToken);

                if (result.IsValid)
                {
                    var idTarefa = await tarefaService.Delete(request.Dto, cancellationToken);
                    return new OkObjectResult(idTarefa);
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

        public async Task<IActionResult> Handle(GetTarefaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tarefaValidator = new TarefaGetDtoValidator();
                var result = await tarefaValidator.ValidateAsync(request.Dto, cancellationToken);

                if (result.IsValid)
                {
                    var tarefas = await tarefaService.GetMany(request.Dto, cancellationToken);
                    return new OkObjectResult(tarefas);
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
