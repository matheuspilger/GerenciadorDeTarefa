namespace GerenciadorDeTarefa.Domain.Helpers
{
    public static class MimeTypeHelper
    {
        private static Dictionary<string, string> BuscarMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            };
        }

        public static string BuscarMimeTypePorExtencao(string extension)
        {
            var types = BuscarMimeTypes();
            return types.GetValueOrDefault(extension);
        }
    }
}
