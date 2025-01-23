namespace GerenciadorDeTarefa.Domain.Configurations
{
    public class ReportConfiguration
    {
        public string Path { get; set; } = Directory.GetCurrentDirectory();
        public string RelatorioTarefa { get; set; } = default!;

        public string FullPath(string custom, string report) => $@"{Path}/{custom}-{report}";
    }
}
