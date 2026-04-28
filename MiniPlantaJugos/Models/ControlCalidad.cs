using System.ComponentModel.DataAnnotations;
using MiniPlantaJugos.Enums;

namespace MiniPlantaJugos.Models
{
    public class ControlCalidad
    {
        public int Id { get; set; }

        [Display(Name = "Orden de Producción")]
        public int OrdenId { get; set; }
        public Orden? Orden { get; set; }

        [Display(Name = "Resultado")]
        public ResultadoCalidad Resultado { get; set; }

        [Display(Name = "Observación")]
        public string? Observacion { get; set; }

        [Display(Name = "Fecha de Control")]
        [DataType(DataType.Date)]
        public DateTime FechaControl { get; set; }
    }
}
