namespace GerenciadorDeTarefa.Domain.Configurations
{
    public class QueueConfiguration
    {
        public string QueueName { get; set; } = default!;
        public string RoutingKey { get; set; } = default!;
        public string ExchangeName { get; set; } = default!;
        public string ExchangeType { get; set; } = default!;
        public bool Durable { get; set; } = true;
        public bool Exclusive { get; set; } = false;
        public bool AutoDelete { get; set; } = false;
        public IDictionary<string, object> Arguments { get; set; } = default;
    }
}
