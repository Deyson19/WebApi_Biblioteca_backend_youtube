using WebApp_SistemaBiblioteca.DataAccess.Entities;
using WebApp_SistemaBiblioteca.DataAccess.ViewModels;
using WebApp_SistemaBiblioteca.Helpers;

namespace WebApp_SistemaBiblioteca.Services.Contrato
{
    public interface IPrestamoService: BaseRepositorio<Prestamo>
    {
        Task<bool> UsuarioTienePrestamo(string idUsuario);
        Task<Respuesta<Prestamo>> GetById(int id);
        Task<Respuesta<bool>> ExisteUsuario(string idUsuario);
        Task<Respuesta<Prestamo>> Actualizar(ActualizarPrestamoViewModel model);
        Task<Respuesta<Prestamo>> Eliminar(int id);
    }
}
