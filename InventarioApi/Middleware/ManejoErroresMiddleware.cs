// Middleware/ManejoErroresMiddleware.cs
using System.Net;
using System.Text.Json;
using InventarioApi.Services.Exceptions;

namespace InventarioApi.Middleware;

public class ManejoErroresMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ManejoErroresMiddleware> _logger;

    public ManejoErroresMiddleware(RequestDelegate next, ILogger<ManejoErroresMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado");
            await ManejarExcepcionAsync(context, ex);
        }
    }

    private static Task ManejarExcepcionAsync(HttpContext context, Exception ex)
    {
        var (codigo, mensaje) = ex switch
        {
            ArticuloNoEncontradoException => (HttpStatusCode.NotFound, ex.Message),
            ValidacionException => (HttpStatusCode.BadRequest, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "Ocurrió un error inesperado en el servidor")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)codigo;

        var respuesta = JsonSerializer.Serialize(new { error = mensaje });
        return context.Response.WriteAsync(respuesta);
    }
}