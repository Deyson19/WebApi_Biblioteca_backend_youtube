using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp_SistemaBiblioteca.DataAccess.Entities
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Isbn { get; set; }
        [MaxLength(10)]
        public string IdentificacionUsuario { get; set; }
        public required DateTime FechaPrestamo { get; set; }
        public required DateTime FechaMaximaDevolucion { get; set; }
        [ForeignKey("TipoUsuarioId")]
        public int TipoUsuarioId { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
