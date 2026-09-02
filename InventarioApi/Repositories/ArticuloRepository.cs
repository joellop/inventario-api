using InventarioApi.Data;
using InventarioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Repositories
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly InventarioDbContext _context;

        public ArticuloRepository(InventarioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Articulo>> ObtenerTodosAsync()
        {
            return await _context.Articulos.ToListAsync();
        }

        public async Task<Articulo?> ObtenerPorIdAsync(int id)
        {
            return await _context.Articulos.FindAsync(id);
        }

        public async Task<Articulo> AgregarAsync(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
            return articulo;
        }

        public async Task ActualizarAsync(Articulo articulo)
        {
            _context.Entry(articulo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Articulo articulo)
        {
            _context.Articulos.Remove(articulo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Articulos.AnyAsync(a => a.Id == id);
        }
    }
}
