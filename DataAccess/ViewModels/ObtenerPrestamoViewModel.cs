namespace WebApp_SistemaBiblioteca.DataAccess.ViewModels
{
    public class ObtenerPrestamoViewModel
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuarioId { get; set; }
        public string FechaMaximaDevolucion { get; set; }
    }
}
