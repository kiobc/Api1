namespace proyecto12.Modelo
{
    public class Producto
    {
        public int ID { get; init; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public DateTime FechaAlta { get; init; }
        public string SKU { get; init; }

    }
}
