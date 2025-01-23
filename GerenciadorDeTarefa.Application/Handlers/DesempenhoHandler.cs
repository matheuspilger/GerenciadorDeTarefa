using GerenciadorDeTarefa.Application.Dtos.Relatorios;
using GerenciadorDeTarefa.Application.Services.Interfaces;
using GerenciadorDeTarefa.Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefa.Application.Handlers
{
    public record GerarRelatorioTarefaCommand(FiltroRelatorioTarefaDto Dto) : IRequest<IActionResult>;

    public class DesempenhoHandler(IDesempenhoService desempenhoService) :
        IRequestHandler<GerarRelatorioTarefaCommand, IActionResult>
    {
        public async Task<IActionResult> Handle(GerarRelatorioTarefaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var projetoValidator = new FiltroRelatorioTarefaDtoValidator();

                var result = await projetoValidator.ValidateAsync(request.Dto ?? new(), cancellationToken);

                if (result.IsValid)
                {
                    var relatorio = await desempenhoService.GerarRelatorioDeTarefas(request.Dto, cancellationToken);
                    return new OkObjectResult(relatorio);
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
