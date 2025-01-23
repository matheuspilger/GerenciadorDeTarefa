namespace GerenciadorDeTarefa.Domain.Helpers
{
    public static class SheetHelper
    {
        public static string GetColumnPosition(this int position, int line)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var value = letters[position - 1];

            return $"{value}{line}";
        }
    }
}
