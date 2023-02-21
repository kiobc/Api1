using api2023.Models;
using Microsoft.AspNetCore.Mvc;

namespace api2023.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {
            List<Cliente> clientes = new List<Cliente>
            {
                new Cliente
                {
                    id="1",
                    correo="google@gmail.com",
                    nombre="Brayan Calderon",
                    edad="30"
                },

                new Cliente
                {
                    id="2",
                    correo="jeff@gmail.com",
                    nombre="Jeff Tutillo",
                    edad="27"
                }
            };

            return clientes;
        }

        [HttpGet]
        [Route("listarxid")]
        public dynamic listarClientexid(int codigo)
        { 
           
                return new Cliente
                {
                    id = codigo.ToString(),
                    correo = "google@gmail.com",
                    nombre = "john Calderon",
                    edad = "30"
                };
        }
    

        [HttpPost]
        [Route("guardar")]
        public dynamic guardarCliente(Cliente cliente)
        {
            cliente.id = "3";
            return new
            {
                success = true,
                message ="cliente registrado",
                result= cliente
            };
        }

        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarCliente(Cliente cliente)
        {
            string token= Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
            if (token != "bray123.")
            {
                return new
                {
                    success = false,
                    message = "token incorrecto",
                    result = ""
                };
            }
            return new
            {
                success = true,
                message = "cliente eliminado",
                result = cliente
            };
        }
    }
}
