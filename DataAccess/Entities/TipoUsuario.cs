using System.ComponentModel.DataAnnotations;

namespace WebApp_SistemaBiblioteca.DataAccess.Entities
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }
        public required string Nombre { get; set; }
    }
}
