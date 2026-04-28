using System.ComponentModel.DataAnnotations;

namespace MiniPlantaJugos.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre del Jugo")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Tipo de Envase")]
        public string TipoEnvase { get; set; } = string.Empty;

        [Display(Name = "Precio Estimado")]
        [DataType(DataType.Currency)]
        public decimal PrecioEstimado { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; } = true;

    }
}
