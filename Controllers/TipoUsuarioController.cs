using Microsoft.AspNetCore.Mvc;
using WebApp_SistemaBiblioteca.DataAccess.Entities;
using WebApp_SistemaBiblioteca.Services.Contrato;

namespace WebApp_SistemaBiblioteca.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class TipoUsuarioController(ITipoUsuarioService tipoUsuarioService) : ControllerBase
     {
          private readonly ITipoUsuarioService _tipoUsuarioService = tipoUsuarioService;

          [HttpGet]
          public async Task<IActionResult> Get()
          {
               var response = await _tipoUsuarioService.GetAll();
               if (response != null)
               {
                    return StatusCode(StatusCodes.Status200OK, response);
               }
               return StatusCode(StatusCodes.Status202Accepted, response);
          }
          [HttpGet("{id}")]
          public async Task<IActionResult> Get(int id)
          {
               var response = await _tipoUsuarioService.GetById(id);
               if (response.IsSuccess)
               {
                    return StatusCode(StatusCodes.Status200OK, response);
               }
               return StatusCode(StatusCodes.Status202Accepted, response);
          }
          [HttpPost]
          public async Task<IActionResult> Post(string nombre)
          {
               if (ModelState.IsValid)
               {
                    var nuevoTipo = await _tipoUsuarioService.Create(new TipoUsuario
                    {
                         Nombre = nombre
                    });
                    return nuevoTipo.IsSuccess ? Ok(nuevoTipo) : BadRequest(nuevoTipo);
               }
               return BadRequest(new
               {
                    Message = "El nombre es requerido"
               });
          }


     }
}