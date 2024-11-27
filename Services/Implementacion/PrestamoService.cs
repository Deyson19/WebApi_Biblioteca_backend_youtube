using Microsoft.EntityFrameworkCore;
using WebApp_SistemaBiblioteca.DataAccess;
using WebApp_SistemaBiblioteca.DataAccess.Entities;
using WebApp_SistemaBiblioteca.DataAccess.ViewModels;
using WebApp_SistemaBiblioteca.Helpers;
using WebApp_SistemaBiblioteca.Services.Contrato;

namespace WebApp_SistemaBiblioteca.Services.Implementacion
{
     public class PrestamoService : IPrestamoService
     {
          private readonly ApplicationDbContext _dbContext;
          public PrestamoService(ApplicationDbContext context)
          {
               _dbContext = context;
          }
          public async Task<Respuesta<Prestamo>> Actualizar(ActualizarPrestamoViewModel model)
          {
               if (model.Id == 0)
               {
                    return new Respuesta<Prestamo>
                    {

                         Message = "El id no es correcto",
                         Result = null,
                         IsSuccess = false
                    };
               }

               var prestamo = await _dbContext.Prestamos.FirstOrDefaultAsync(x => x.Id == model.Id);
               if (prestamo != null)
               {
                    prestamo.Isbn = model.Isbn;
                    prestamo.IdentificacionUsuario = model.IdentificacionUsuario;
                    prestamo.TipoUsuarioId = model.TipoUsuarioId;

                    _dbContext.Prestamos.Update(prestamo);
                    await _dbContext.SaveChangesAsync();

                    return new Respuesta<Prestamo>
                    {
                         IsSuccess = true,
                         Message = "Se actualizó el prestamo",
                         Result = prestamo
                    };
               }
               else
               {
                    return new Respuesta<Prestamo>
                    {

                         Message = "No hay un prestamo",
                         Result = null,
                         IsSuccess = false
                    };
               }
          }

          public async Task<Respuesta<Prestamo>> Create(Prestamo entidad)
          {
               if (entidad == null)
               {
                    return new Respuesta<Prestamo>
                    {
                         IsSuccess = false,
                         Message = "No es un modelo correcto",
                         Result = entidad
                    };
               }
               else
               {
                    await _dbContext.Prestamos.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();
                    return new Respuesta<Prestamo>
                    {
                         IsSuccess = true,
                         Message = "Se creo el prestamo",
                         Result = entidad
                    };
               }
          }

          public async Task<Respuesta<Prestamo>> Eliminar(int id)
          {
               if (id == 0)
               {
                    return new Respuesta<Prestamo>
                    {
                         IsSuccess = false,
                         Message = "El id es requerido",
                         Result = null
                    };
               }
               else
               {
                    var prestamo = await _dbContext.Prestamos.FirstOrDefaultAsync(x => x.Id == id);
                    if (prestamo == null)
                    {
                         return new Respuesta<Prestamo>
                         {
                              IsSuccess = false,
                              Message = "El prestamo no existe",
                              Result = null
                         };
                    }
                    else
                    {
                         _dbContext.Prestamos.Remove(prestamo);
                         await _dbContext.SaveChangesAsync();
                         return new Respuesta<Prestamo>
                         {
                              IsSuccess = true,
                              Message = "Se eliminó un registro",
                              Result = prestamo
                         };
                    }
               }
          }

          public async Task<Respuesta<bool>> ExisteUsuario(string idUsuario)
          {
               var usuario = await _dbContext.Prestamos.FirstOrDefaultAsync(x => x.IdentificacionUsuario.ToLower() == idUsuario.ToLower());
               if (usuario == null)
               {
                    return new Respuesta<bool>
                    {
                         Message = "No existe el usuario",
                         Result = false,
                         IsSuccess = false
                    };
               }
               else
               {
                    return new Respuesta<bool>
                    {
                         IsSuccess = true,
                         Result = true,
                         Message = "Se ha encontrado un usuario"
                    };
               }
          }

          public async Task<Respuesta<IEnumerable<Prestamo>>> GetAll()
          {
               var listado = await _dbContext.Prestamos.ToListAsync();
               return new Respuesta<IEnumerable<Prestamo>>
               {
                    IsSuccess = listado.Any(),
                    Message = listado.Any() ? "Listado de prestamos" : "No hay prestamos",
                    Result = listado
               };
          }

          public async Task<Respuesta<Prestamo>> GetById(int id)
          {
               if (id == 0)
               {
                    return new Respuesta<Prestamo>
                    {
                         IsSuccess = false,
                         Message = "No es correcto el id",
                         Result = null
                    };
               }
               else
               {
                    var prestamo = await GetPrestamo(id);
                    return new Respuesta<Prestamo>
                    {
                         IsSuccess = prestamo != null,
                         Message = prestamo != null ? "Se ha encontrado un registro" : "No hay resultados",
                         Result = prestamo
                    };
               }
          }

          public async Task<bool> UsuarioTienePrestamo(string idUsuario)
          {
               if (string.IsNullOrEmpty(idUsuario))
               {
                    return false;
               }
               var prestamo = await _dbContext.Prestamos.Where(
                    p => p.FechaMaximaDevolucion > DateTime.Now
               ).FirstOrDefaultAsync(x => x.IdentificacionUsuario == idUsuario);

               if (prestamo == null)
               {
                    return false;
               }
               else
               {
                    return true;
               }
          }

          private async Task<Prestamo> GetPrestamo(int id)
          {
               var prestamo = await _dbContext.Prestamos.FirstOrDefaultAsync(x => x.Id == id);
               return prestamo!;
          }
     }



















}