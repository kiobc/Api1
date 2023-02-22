using proyecto12.Modelo;

namespace proyecto12.Repositorio
{
    public interface IProductosEnMemoria
    {
       Task <Producto> DameProductoAsincrono(string SKU);
       Task  <IEnumerable<Producto>> DameProductosAsincrono(int pag, int reg);
        Task  CrearProductoAsincrono(Producto p);
        Task  ModificarProductoAsincrono(Producto p);
        Task BorrarProductoAsincrono(string codProducto);
    }
}