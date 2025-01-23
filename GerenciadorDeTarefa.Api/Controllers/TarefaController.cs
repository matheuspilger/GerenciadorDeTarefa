using GerenciadorDeTarefa.Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController(IMediator mediator) : ControllerBase
    {
        [HttpPost(nameof(AddOrUpdateTarefa))]
        public async Task<IActionResult> AddOrUpdateTarefa([FromBody] AddOrUpdateTarefaCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpDelete(nameof(DeleteTarefa))]
        public async Task<IActionResult> DeleteTarefa([FromQuery] DeleteTarefaCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet(nameof(GetTarefas))]
        public async Task<IActionResult> GetTarefas([FromQuery] GetTarefaCommand command)
        {
            return await mediator.Send(command);
        }
    }
}
