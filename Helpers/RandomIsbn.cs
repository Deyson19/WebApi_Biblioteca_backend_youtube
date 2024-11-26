namespace WebApp_SistemaBiblioteca.Helpers
{
    public static class RandomIsbn
    {
        private static readonly Random random = new Random();

        public static string GenerarIsbn(string isbActual)
        {
            //Cantidad de caracteres a generar
            int cantidad = 4;
            //Abecedario
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var complementarIsbn = new string(Enumerable.Repeat(caracteres,cantidad)
                .Select(c => c[random.Next(c.Length)]).ToArray());

            return $"{complementarIsbn}{isbActual}";
        }
    }
}
