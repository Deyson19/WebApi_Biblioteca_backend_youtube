using System.ComponentModel.DataAnnotations;

namespace WebApp_SistemaBiblioteca.DataAccess.ViewModels
{
    public class CrearPrestamoViewModel
    {
        [MaxLength(6,ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Isbn { get; set; }
        [MaxLength(10, ErrorMessage = "El campo {0} no puede superar los {1} caracteres")]
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuarioId { get; set; }
    }
    public class ActualizarPrestamoViewModel
    {
        [MaxLength(10,ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Isbn { get; set; }
        [MaxLength(10, ErrorMessage = "El campo {0} no puede superar los {1} caracteres")]
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuarioId { get; set; }
    }
}
