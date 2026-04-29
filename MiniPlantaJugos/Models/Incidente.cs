using System.ComponentModel.DataAnnotations;
using MiniPlantaJugos.Enums;

namespace MiniPlantaJugos.Models
{
    public class Incidente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una máquina")]
        [Display(Name = "Máquina")]
        public int MaquinaId { get; set; }
        public Maquina? Maquina { get; set; }

        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "La descripción del problema es obligatoria")]
        [Display(Name = "Descripción del Problema")]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Severidad")]
        public Severidad Severidad { get; set; }

        [Display(Name = "Estado")]
        public EstadoIncidente Estado { get; set; } = EstadoIncidente.Abierto;
    }
}
