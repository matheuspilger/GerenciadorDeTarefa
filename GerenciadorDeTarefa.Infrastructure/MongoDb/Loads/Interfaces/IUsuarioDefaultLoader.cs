namespace GerenciadorDeTarefa.Infrastructure.MongoDb.Loads.Interfaces
{
    public interface IUsuarioDefaultLoader
    {
        Task Load(CancellationToken token);
    }
}
