namespace InventarioApi.Models
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal? ValorEstimado { get; set; }
        public DateTime? FechaAdquisicion { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public string? Notas { get; set; }
    }
}
