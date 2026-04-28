using System.ComponentModel.DataAnnotations;
using MiniPlantaJugos.Enums;

namespace MiniPlantaJugos.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Cargo")]
        public Cargo Cargo { get; set; }

        // Navegación many-to-many con Maquina
        public ICollection<Maquina> Maquinas { get; set; } = new List<Maquina>();
        public ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
        public ICollection<Incidente> Incidentes { get; set; } = new List<Incidente>();
    }
}
