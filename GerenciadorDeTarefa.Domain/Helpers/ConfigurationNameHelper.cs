namespace GerenciadorDeTarefa.Domain.Helpers
{
    public static class ConfigurationNameHelper
    {
        public static string GetQueueConfig(this Type type)
            => $"Queue{type.Name}";

        public static string GetConsumerConfig(this Type type)
            => $"Consumer{type.Name}";

        public static string GetPublishConfig(this Type type)
            => $"Publish{type.Name}";
    }
}
