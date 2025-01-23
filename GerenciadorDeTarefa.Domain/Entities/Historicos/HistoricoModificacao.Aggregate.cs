namespace GerenciadorDeTarefa.Domain.Entities.Historicos
{
    public partial class HistoricoModificacao
    {
        public HistoricoModificacao(string propriedade, object valorOriginal, object valorAlterado)
        {
            Propriedade = propriedade;
            ValorOriginal = valorOriginal;
            ValorAlterado = valorAlterado;
        }
    }
}
