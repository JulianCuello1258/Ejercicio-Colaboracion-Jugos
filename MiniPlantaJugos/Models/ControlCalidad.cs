using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiniPlantaJugos.Enums;

namespace MiniPlantaJugos.Models
{
    public class ControlCalidad
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una producción a revisar")]
        [Display(Name = "Producción Revisada")]
        public int OrdenProdId { get; set; }

        [ForeignKey("OrdenProdId")]
        public OrdenProd? ProdRevisada { get; set; }

        [Required(ErrorMessage = "Debe indicar el resultado del control")]
        [Display(Name = "Resultado")]
        public ResultadoControl Resultado { get; set; } = ResultadoControl.Aprobado;

        [Required(ErrorMessage = "La fecha del control es obligatoria")]
        [Display(Name = "Fecha de Control")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [StringLength(500, ErrorMessage = "La observación no puede exceder los 500 caracteres")]
        [Display(Name = "Observación")]
        public string? Observacion { get; set; }
    }
}
