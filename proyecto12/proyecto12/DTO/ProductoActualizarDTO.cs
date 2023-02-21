using System.ComponentModel.DataAnnotations;

namespace proyecto12.DTO
{
    public class ProductoActualizarDTO
    {
         
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        [Range(5, 99,
            ErrorMessage = "Valor {0} debe estar entre {1} y {2}. ")]
        public double Precio { get; set; }
        
    }
}

