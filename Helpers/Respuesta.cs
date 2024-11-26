namespace WebApp_SistemaBiblioteca.Helpers
{
    public class Respuesta<T>
    {
        public required T Result { get; set; }
        public bool IsSuccess { get; set; }
        public required string Message { get; set; } = string.Empty;
    }
}
