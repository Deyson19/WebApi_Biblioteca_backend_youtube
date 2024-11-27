using Microsoft.EntityFrameworkCore;
using WebApp_SistemaBiblioteca.DataAccess;
using WebApp_SistemaBiblioteca.DataAccess.Entities;
using WebApp_SistemaBiblioteca.Helpers;
using WebApp_SistemaBiblioteca.Services.Contrato;

namespace WebApp_SistemaBiblioteca.Services.Implementacion
{
    public class TipoUsuarioService(ApplicationDbContext dbContext) : ITipoUsuarioService
    {
        private readonly ApplicationDbContext _context = dbContext;
        public async Task<Respuesta<TipoUsuario>> Create(TipoUsuario entidad)
        {
            if (entidad == null)
            {
                return new Respuesta<TipoUsuario>
                {
                    IsSuccess = false,
                    Message = "El modelo ingresado no es correcto",
                    Result = entidad
                };
            }
            else
            {
                await _context.TipoUsuarios.AddAsync(entidad);
                await _context.SaveChangesAsync();

                return new Respuesta<TipoUsuario>
                {
                    IsSuccess = true,
                    Message = "Se ha creado un nuevo tipo de usuario",
                    Result = entidad
                };
            }
        }

        public async Task<Respuesta<IEnumerable<TipoUsuario>>> GetAll()
        {
            var listado = await _context.TipoUsuarios.ToListAsync();

            //*usando operador ternario
            return new Respuesta<IEnumerable<TipoUsuario>>
            {
                IsSuccess = listado.Any(),
                Message = listado.Any() ? "Listado de tipos de usuario" : "No hay registros",
                Result = listado
            };
        }

        public async Task<Respuesta<TipoUsuario>> GetById(int id)
        {
            var tipoUsuario = await _context.TipoUsuarios.FirstOrDefaultAsync(x=>x.Id ==id);
            //var tipoUsuario = await _context.TipoUsuarios.FindAsync(id);

            return new Respuesta<TipoUsuario>
            {
                IsSuccess = tipoUsuario != null,
                Message = tipoUsuario != null ? "Tipo de usuario" : "No hay resultados",
                Result = tipoUsuario
            };
        }
    }
}
