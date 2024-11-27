using Microsoft.AspNetCore.Mvc;
using WebApp_SistemaBiblioteca.DataAccess.Entities;
using WebApp_SistemaBiblioteca.DataAccess.ViewModels;
using WebApp_SistemaBiblioteca.Helpers;
using WebApp_SistemaBiblioteca.Services.Contrato;

namespace WebApp_SistemaBiblioteca.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class PrestamoController : ControllerBase
     {
          private readonly ITipoUsuarioService _tipoUsuarioService;
          private readonly IPrestamoService _prestamoService;

          public PrestamoController(ITipoUsuarioService tipoUsuarioService,
          IPrestamoService prestamoService)
          {
               _tipoUsuarioService = tipoUsuarioService;
               _prestamoService = prestamoService;
          }

          //*Obtener todos
          [HttpGet]
          public async Task<IActionResult> Get()
          {
               var response = await _prestamoService.GetAll();
               if (response.IsSuccess)
               {
                    return StatusCode(StatusCodes.Status200OK, response);
               }
               return StatusCode(StatusCodes.Status204NoContent, response);
          }
          [HttpGet("{idPrestamo}")]
          public async Task<IActionResult> Get(int idPrestamo)
          {
               if (!ModelState.IsValid)
               {
                    return BadRequest(new
                    {
                         Message = "No es correcto el id",
                         IsSuccess = false
                    });
               }
               var prestamo = await _prestamoService.GetById(idPrestamo);
               if (prestamo.IsSuccess)
               {
                    var prestamoById = new ObtenerPrestamoViewModel
                    {
                         Id = prestamo.Result.Id,
                         Isbn = prestamo.Result.Isbn,
                         FechaMaximaDevolucion = prestamo.Result.FechaMaximaDevolucion.ToString("f"),
                         TipoUsuarioId = prestamo.Result.TipoUsuarioId,
                         IdentificacionUsuario = prestamo.Result.IdentificacionUsuario
                    };
                    return Ok(prestamoById);
               }
               return NotFound(new
               {
                    Message = "No hay resultados",
                    IsSuccess = false
               });
          }
          [HttpDelete("{idPrestamo}")]
          public async Task<IActionResult> Delete(int idPrestamo)
          {
               if (!ModelState.IsValid)
               {
                    return BadRequest(new
                    {
                         Message = "El id no es correcto",
                         IsSuccess = false
                    });
               }
               var eliminar = await _prestamoService.Eliminar(idPrestamo);
               if (eliminar.IsSuccess)
               {
                    return Ok(eliminar);
               }
               else
               {
                    return StatusCode(StatusCodes.Status400BadRequest, eliminar);
               }
          }

          [HttpPut("{idPrestamo}")]
          public async Task<IActionResult> Update(int idPrestamo, [FromBody] ActualizarPrestamoViewModel model)
          {
               if (idPrestamo != model.Id)
               {
                    return BadRequest(new
                    {
                         Message = "Los datos no son correctos",
                         IsSuccess = false
                    });
               }
               if (!ModelState.IsValid)
               {
                    return BadRequest(new
                    {
                         Message = "Los datos no son correctos",
                         IsSuccess = false
                    });
               }
               else
               {
                    var prestamo = await _prestamoService.Actualizar(model);
                    if (prestamo.IsSuccess)
                    {
                         return Ok(prestamo);
                    }
                    return NotFound(new
                    {
                         prestamo.Message,
                    });
               }
          }

          [HttpPost]
          public async Task<IActionResult> Post([FromBody] CrearPrestamoViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    return BadRequest(new
                    {
                         Message = "Los datos no son correctos",
                         IsSuccess = false
                    });
               }
               //*Validar si existe un tipo de usuario
               var existeTipoUsuario = await ObtenerTipoUsuario(model.TipoUsuarioId);
               if (existeTipoUsuario == null)
               {
                    return BadRequest(new
                    {
                         Message = "El tipo de usuario no existe",
                         IsSuccess = false
                    });
               }
               //*Evaluar si usuario tiene prestamo
               if (existeTipoUsuario.Id == 3)
               {
                    var existePrestamo = await UsuarioTientePrestamo(model.IdentificacionUsuario);
                    if (existePrestamo)
                    {
                         return BadRequest(new
                         {
                              Message = $"El usuario con documento: {model.IdentificacionUsuario} ya tiene un prestamo.",
                              IsSuccess = false
                         });
                    }
                    var nuevoPrestamoInvitado = CrearPrestamo(model);
                    if (nuevoPrestamoInvitado != null)
                    {
                         var crearPrestamo = await _prestamoService.Create(nuevoPrestamoInvitado);
                         if (crearPrestamo.IsSuccess)
                         {
                              return Ok(nuevoPrestamoInvitado);
                         }
                         return BadRequest(nuevoPrestamoInvitado);
                    }
                    return BadRequest(new
                    {
                         Message = "No se pudo crear el registro"
                    });
               }
               var nuevoPrestamo = CrearPrestamo(model);
               if (nuevoPrestamo == null) { return BadRequest(new { Message = "No se pudo crear" }); }

               var resultado = await _prestamoService.Create(nuevoPrestamo);
               if (resultado.IsSuccess)
               {
                    return Ok(resultado);
               }
               return BadRequest(resultado);
          }

          #region Private Methods
          //? Por qué creo metodos privados
          //*Se crean para no repetir mucha lógica de negocio
          //*Los metodos privados no se listan en la api, ni en la documentacion

          private async Task<bool> UsuarioTientePrestamo(string idUsuario) => await _prestamoService.UsuarioTienePrestamo(idUsuario);

          private async Task<TipoUsuario> ObtenerTipoUsuario(int id)
          {
               var resultado = await _tipoUsuarioService.GetById(id);
               return resultado.Result;
          }

          private Prestamo CrearPrestamo(CrearPrestamoViewModel model)
          {
               if (model == null)
               {
                    return null!;
               }
               else
               {
                    return new Prestamo
                    {
                         IdentificacionUsuario = model.IdentificacionUsuario,
                         Isbn = model.Isbn,
                         FechaMaximaDevolucion = CantidadMaximaPrestamo.FechaMaximaPrestamo(model.TipoUsuarioId),
                         TipoUsuarioId = model.TipoUsuarioId,
                         FechaPrestamo = DateTime.Now,
                    };
               }
          }
          #endregion
     }
}