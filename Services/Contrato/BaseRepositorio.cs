using WebApp_SistemaBiblioteca.Helpers;

namespace WebApp_SistemaBiblioteca.Services.Contrato
{
    public interface BaseRepositorio<T>
    {
        public Task<Respuesta<T>> Create(T entidad);
        public Task<Respuesta<IEnumerable<T>>> GetAll();
    }
}
