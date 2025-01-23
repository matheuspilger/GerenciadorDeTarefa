using GerenciadorDeTarefa.Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesempenhoController(IMediator mediator) : ControllerBase
    {
        [HttpGet(nameof(GerarRelatorioTarefas))]
        public async Task<IActionResult> GerarRelatorioTarefas([FromQuery] GerarRelatorioTarefaCommand command)
        {
            return await mediator.Send(command);
        }
    }
}
