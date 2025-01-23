namespace GerenciadorDeTarefa.Domain.Entities.Historicos
{
    public partial class HistoricoModificacao
    {
        public string Propriedade { get; private set; }
        public object ValorOriginal { get; private set; }
        public object ValorAlterado { get; private set; }
    }
}
