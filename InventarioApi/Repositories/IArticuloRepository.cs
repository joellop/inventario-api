using InventarioApi.Models;

namespace InventarioApi.Repositories
{
    public interface IArticuloRepository
    {
        Task<IEnumerable<Articulo>> ObtenerTodosAsync();
        Task<Articulo?> ObtenerPorIdAsync(int id);
        Task<Articulo> AgregarAsync(Articulo articulo);
        Task ActualizarAsync(Articulo articulo);
        Task EliminarAsync(Articulo articulo);
        Task<bool> ExisteAsync(int id);
    }
}
