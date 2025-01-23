using GerenciadorDeTarefa.Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController(IMediator mediator) : ControllerBase
    {
        [HttpPost(nameof(AddOrUpdateProjeto))]
        public async Task<IActionResult> AddOrUpdateProjeto([FromBody] AddOrUpdateProjetoCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpDelete(nameof(DeleteProjeto))]
        public async Task<IActionResult> DeleteProjeto([FromQuery] DeleteProjetoCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpGet(nameof(GetProjetos))]
        public async Task<IActionResult> GetProjetos([FromQuery] GetProjetoCommand command)
        {
            return await mediator.Send(command);
        }
    }
}
