namespace InventarioApi.Services.Exceptions
{
    public class ArticuloNoEncontradoException : Exception
    {
        public ArticuloNoEncontradoException(int id): 
            base($"No se encontró el artículo con id {id}") { }
    }
}
