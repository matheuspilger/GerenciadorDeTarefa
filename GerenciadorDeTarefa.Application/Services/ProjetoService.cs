using GerenciadorDeTarefa.Application.Dtos.Projetos;
using GerenciadorDeTarefa.Application.Services.Interfaces;
using GerenciadorDeTarefa.Domain.Entities.Projetos;
using GerenciadorDeTarefa.Domain.Helpers;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories;

namespace GerenciadorDeTarefa.Application.Services
{
    public class ProjetoService(
        IUsuarioRepository usuarioRepository,
        IProjetoRepository projetoRepository) : IProjetoService
    {
        public async Task<string> AddOrUpdate(ProjetoDto projetoDto, CancellationToken token)
        {
            var usuario = await usuarioRepository.FindOneBy(u => u.Id == projetoDto.IdUsuario.ToObjectId(), token);
            var projeto = Projeto.Builder.Build(projetoDto.Id, projetoDto.Nome, projetoDto.Sobre, usuario);
            await projetoRepository.AddOrUpdateBy(projeto, x => x.Id == projeto.Id, token);
            return projeto.Id.ToString();
        }

        public async Task<string> Delete(ProjetoDeleteDto projetoDto, CancellationToken token)
        {
            var projeto = await projetoRepository.FindOneBy(p => p.Id == projetoDto.IdProjeto.ToObjectId(), token);
            if (projeto.IsDeletable())
                await projetoRepository.DeleteBy(x => x.Id == projeto.Id, token);
            return projeto.Id.ToString();
        }

        public async Task<List<ProjetoDto>> GetMany(ProjetoGetDto projetoDto, CancellationToken token)
        {
            var projetos = await projetoRepository.FindManyBy(p => p.Usuario.Id == projetoDto.IdUsuario.ToObjectId(), token);
            return projetos.Select(p => new ProjetoDto { Id = p.Id.ToString(), IdUsuario = p.Usuario.Id.ToString(), Nome = p.Nome, Sobre = p.Sobre }).ToList();
        }
    }
}
