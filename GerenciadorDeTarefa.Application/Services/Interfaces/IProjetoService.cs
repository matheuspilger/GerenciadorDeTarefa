using GerenciadorDeTarefa.Application.Dtos.Projetos;

namespace GerenciadorDeTarefa.Application.Services.Interfaces
{
    public interface IProjetoService
    {
        Task<string> AddOrUpdate(ProjetoDto projetoDto, CancellationToken token);
        Task<string> Delete(ProjetoDeleteDto projetoDto, CancellationToken token);
        Task<List<ProjetoDto>> GetMany(ProjetoGetDto projetoDto, CancellationToken token);
    }
}
