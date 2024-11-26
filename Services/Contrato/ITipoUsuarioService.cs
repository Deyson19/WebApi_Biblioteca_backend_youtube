using WebApp_SistemaBiblioteca.DataAccess.Entities;
using WebApp_SistemaBiblioteca.Helpers;

namespace WebApp_SistemaBiblioteca.Services.Contrato
{
    public interface ITipoUsuarioService : BaseRepositorio<TipoUsuario>
    {
        public Task<Respuesta<TipoUsuario>> GetById(int id);
    }
}
