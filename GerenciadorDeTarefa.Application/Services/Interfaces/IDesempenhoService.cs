using GerenciadorDeTarefa.Application.Dtos.Relatorios;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefa.Application.Services.Interfaces
{
    public interface IDesempenhoService
    {
        Task<FileContentResult> GerarRelatorioDeTarefas(FiltroRelatorioTarefaDto filtroDto, CancellationToken token);
    }
}
