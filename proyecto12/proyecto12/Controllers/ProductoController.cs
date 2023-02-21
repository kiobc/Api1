using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyecto12.DTO;
using proyecto12.Modelo;
using proyecto12.Repositorio;

namespace proyecto12.Controllers
{
    [Route("productos")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoController : ControllerBase
    {
        private readonly IProductosEnMemoria repositorio;

        public ProductoController(IProductosEnMemoria r)
        {
            this.repositorio = r;
        }

        [HttpGet]
        [Authorize]
        public async Task <IEnumerable<ProductoDTO>> DameProductos()
        {
            var listaProductos = (await repositorio.DameProductosAsincrono()).Select(p => p.convertirDTO());
            return listaProductos;
        }

        [HttpGet("{codProducto}")]
        [Authorize]
        public async Task <ActionResult<ProductoDTO>> DameProducto(string codProducto)
        {
            var producto = (await repositorio.DameProductoAsincrono(codProducto)).convertirDTO();
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }
        [HttpPost]
        [Authorize]
        public ActionResult<ProductoDTO> CrearProducto(ProductoDTO p)
        {
            Producto producto = new Producto
            {
                
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                SKU = p.SKU,
                FechaAlta = DateTime.Now,
            };
            repositorio.CrearProductoAsincrono(producto);
            return producto.convertirDTO();
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductoDTO>> ModificarProducto(String codProducto, ProductoActualizarDTO p)
        {
            Producto existeProducto = await repositorio.DameProductoAsincrono(codProducto);
            if (existeProducto == null)
            {
                return NotFound();
            }
            existeProducto.Nombre = p.Nombre;
            existeProducto.Descripcion = p.Descripcion;
            existeProducto.Precio = p.Precio;

            await repositorio.ModificarProductoAsincrono(existeProducto);
            return existeProducto.convertirDTO();
        }
        [HttpDelete]
        [Authorize]
        public async Task <ActionResult> BorrarProducto(String codProducto)
        {
            Producto existeProducto =await  repositorio.DameProductoAsincrono(codProducto);
            if (existeProducto is null)
            {
                return NotFound();
            }
            await repositorio.BorrarProductoAsincrono(codProducto);
            return NoContent();
        }
    }
}
