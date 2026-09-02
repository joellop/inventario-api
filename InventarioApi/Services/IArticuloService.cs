using InventarioApi.Models;

namespace InventarioApi.Services
{
    public interface IArticuloService
    {
        Task<IEnumerable<Articulo>> ObtenerTodosAsync();
        Task<Articulo> ObtenerPorIdAsync(int id);
        Task<Articulo> CrearAsync(Articulo articulo);
        Task<bool> ActualizarAsync(int id, Articulo articulo);
        Task<bool> EliminarAsync(int id);
    }
}
