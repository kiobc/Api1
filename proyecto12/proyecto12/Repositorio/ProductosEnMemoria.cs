
using proyecto12.Modelo;

namespace proyecto12.Repositorio
{
    public class ProductosEnMemoria: IProductosEnMemoria
    {
        private readonly List<Producto> productos = new()
        {
new Producto {ID=1, Nombre="Martillo", Descripcion="Martillo super preciso", Precio=12.99, FechaAlta=DateTime.Now, SKU = "MART01"},
new Producto {ID=2, Nombre="Caja de Clavos", Descripcion="100 unidades de clavos", Precio=10, FechaAlta=DateTime.Now, SKU = "BART01"},
new Producto {ID=3, Nombre="Destornillador", Descripcion="Excelente destornillador",Precio=9.99, FechaAlta=DateTime.Now,SKU = "CART01"},
       new Producto {
           ID=4,
           Nombre="Bombilla",
           Descripcion="Bombilla muy luminosa",
           Precio=3,
           FechaAlta=DateTime.Now,SKU = "SART01"},
        };

        public string SKU { get; private set; }

        public async Task <IEnumerable<Producto>> DameProductosAsincrono ()
        {
            return await Task.FromResult (productos);
        }
        public async Task <Producto> DameProductoAsincrono(string SKU)
        {
            return await Task.FromResult (productos.Where(p => p.SKU == SKU).SingleOrDefault());
    ;        }


        public async Task CrearProductoAsincrono(Producto p)
        {
            productos.Add(p);
        }

        public async Task ModificarProductoAsincrono(Producto p)
        {
            int indice = productos.FindIndex(existeProducto => existeProducto.ID == p.ID);
            productos[indice] = p;
        }

        public async Task BorrarProductoAsincrono(String SKU)
        {
            int indice = productos.FindIndex(existeProducto => existeProducto.SKU == SKU);
            productos.RemoveAt(indice);
        }

        Task IProductosEnMemoria.CrearProductoAsincrono(Producto p)
        {
            throw new NotImplementedException();
        }

        Task IProductosEnMemoria.ModificarProductoAsincrono(Producto p)
        {
            throw new NotImplementedException();
        }

        Task IProductosEnMemoria.BorrarProductoAsincrono(string codProducto)
        {
            throw new NotImplementedException();
        }
    }
}
