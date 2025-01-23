namespace GerenciadorDeTarefa.Domain.Entities.Historicos
{
    public partial class HistoricoModificacao
    {
        public static class Builder
        {
            public static HistoricoModificacao Build(string propriedade, object valorOriginal, object valorAlterado)
                => new(propriedade, valorOriginal, valorAlterado);
        }
    }
}
