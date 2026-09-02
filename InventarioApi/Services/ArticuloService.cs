using InventarioApi.Models;
using InventarioApi.Repositories;
using InventarioApi.Services.Exceptions;

namespace InventarioApi.Services;

public class ArticuloService : IArticuloService
{
    private readonly IArticuloRepository _repository;

    public ArticuloService(IArticuloRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Articulo>> ObtenerTodosAsync()
    {
        return await _repository.ObtenerTodosAsync();
    }

    public async Task<Articulo> ObtenerPorIdAsync(int id)
    {
        var articulo = await _repository.ObtenerPorIdAsync(id);
        if (articulo == null)
        {
            throw new ArticuloNoEncontradoException(id);
        }
        return articulo;
    }

    public async Task<Articulo> CrearAsync(Articulo articulo)
    {
        if (articulo.Cantidad < 0)
        {
            throw new ValidacionException("La cantidad no puede ser negativa");
        }

        articulo.FechaAdquisicion = NormalizarFechaUtc(articulo.FechaAdquisicion);
        articulo.FechaCaducidad = NormalizarFechaUtc(articulo.FechaCaducidad);

        return await _repository.AgregarAsync(articulo);
    }

    public async Task<bool> ActualizarAsync(int id, Articulo articulo)
    {
        if (id != articulo.Id)
        {
            throw new ValidacionException("El id de la ruta no coincide con el del artículo");
        }

        var existe = await _repository.ExisteAsync(id);
        if (!existe)
        {
            throw new ArticuloNoEncontradoException(id);
        }

        if (articulo.Cantidad < 0)
        {
            throw new ValidacionException("La cantidad no puede ser negativa");
        }

        articulo.FechaAdquisicion = NormalizarFechaUtc(articulo.FechaAdquisicion);
        articulo.FechaCaducidad = NormalizarFechaUtc(articulo.FechaCaducidad);

        await _repository.ActualizarAsync(articulo);
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var articulo = await _repository.ObtenerPorIdAsync(id);
        if (articulo == null)
        {
            throw new ArticuloNoEncontradoException(id);
        }

        await _repository.EliminarAsync(articulo);
        return true;
    }

    private static DateTime? NormalizarFechaUtc(DateTime? fecha)
    {
        if (fecha == null) return null;

        return fecha.Value.Kind switch
        {
            DateTimeKind.Utc => fecha.Value,
            DateTimeKind.Local => fecha.Value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(fecha.Value, DateTimeKind.Utc),
        };
    }
}