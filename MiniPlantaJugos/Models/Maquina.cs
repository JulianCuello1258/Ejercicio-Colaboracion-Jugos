using System.ComponentModel.DataAnnotations;
using MiniPlantaJugos.Enums;

namespace MiniPlantaJugos.Models
{
    public class Maquina
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Sector")]
        public string Sector { get; set; } = string.Empty;

        [Display(Name = "Planta")]
        public string Planta { get; set; } = string.Empty;

        [Display(Name = "Número de Máquina")]
        public int NumeroMaquina { get; set; }

        [Display(Name = "Estado")]
        public EstadoMaquina Estado { get; set; } = EstadoMaquina.Operativa;

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

    }
}
