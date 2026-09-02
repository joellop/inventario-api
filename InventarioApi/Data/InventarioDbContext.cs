using InventarioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioApi.Data
{
    public class InventarioDbContext : DbContext
    {
        public InventarioDbContext(DbContextOptions<InventarioDbContext> options)
            : base(options) { }

        public DbSet<Articulo> Articulos { get; set; }
    }
}
