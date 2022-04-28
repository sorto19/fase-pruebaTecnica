using WebUsuarionet.Models;

namespace WebUsuarionet.Servicios
{
    public interface IServicio_Api
    {
        Task<List<Usuario>> lista();
        Task<Usuario> Obtener(int IdUsuario);

        Task<bool> Guardar(Usuario objeto);
        Task<bool> Editar(Usuario objeto);

        Task<bool> Eliminar(int IdUsuario);
    }
}
