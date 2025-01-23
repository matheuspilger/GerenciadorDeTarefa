using GerenciadorDeTarefa.Domain.Entities.Usuarios;
using GerenciadorDeTarefa.Domain.Enums.Usuarios;
using GerenciadorDeTarefa.Domain.Interfaces.Repositories;
using GerenciadorDeTarefa.Infrastructure.MongoDb.Loads.Interfaces;
using MongoDB.Bson;

namespace GerenciadorDeTarefa.Infrastructure.MongoDb.Loads
{
    public class UsuarioDefaultLoader(IUsuarioRepository usuarioRepository) : IUsuarioDefaultLoader
    {
        public async Task Load(CancellationToken token)
        {
            var usuarios = new List<Usuario>
            {
                Usuario.Builder.Build("678efb1a9fd8b0af8f5c9d57", true, "Matheus Teste", "admin", "admin@empresa.com", "123qwe", Funcao.Gerente),
                Usuario.Builder.Build("678efb1a9fd8b0af8f5c9d59", true, "Fulano Teste", "operador1", "operador1@empresa.com", "1234", Funcao.Operador),
                Usuario.Builder.Build("678efb1a9fd8b0af8f5c9d5b", true, "Ciclano Teste", "operador2", "operador2@empresa.com", "1234", Funcao.Operador),
                Usuario.Builder.Build("678efb1a9fd8b0af8f5c9d5d", true, "Beltrano Teste", "operador3", "operador3@empresa.com", "1234", Funcao.Operador)
            };

            foreach (var usuario in usuarios)
                await usuarioRepository.AddOrUpdateBy(usuario, x => x.Id == usuario.Id, token);
        }
    }
}
