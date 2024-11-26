using Microsoft.EntityFrameworkCore;
using WebApp_SistemaBiblioteca.DataAccess.Entities;

namespace WebApp_SistemaBiblioteca.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> op) : base(op)
        {

        }

        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
    }
}
