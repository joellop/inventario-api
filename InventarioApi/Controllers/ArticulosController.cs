using Microsoft.AspNetCore.Mvc;
using InventarioApi.Models;
using InventarioApi.Services;

namespace InventarioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticulosController : ControllerBase
{
    private readonly IArticuloService _service;

    public ArticulosController(IArticuloService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Articulo>>> GetArticulos()
    {
        return Ok(await _service.ObtenerTodosAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Articulo>> GetArticulo(int id)
    {
        return Ok(await _service.ObtenerPorIdAsync(id));
    }

    [HttpPost]
    public async Task<ActionResult<Articulo>> PostArticulo(Articulo articulo)
    {
        var creado = await _service.CrearAsync(articulo);
        return CreatedAtAction(nameof(GetArticulo), new { id = creado.Id }, creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutArticulo(int id, Articulo articulo)
    {
        var actualizado = await _service.ActualizarAsync(id, articulo);
        return Ok(actualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticulo(int id)
    {
        var eliminado = await _service.EliminarAsync(id);
        return Ok(eliminado);
    }
}