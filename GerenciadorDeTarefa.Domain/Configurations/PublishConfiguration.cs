namespace GerenciadorDeTarefa.Domain.Configurations
{
    public class PublishConfiguration<T>
    {
        public bool Mandatary { get; set; } = false;
        public T Properties { get; set; }
    }
}