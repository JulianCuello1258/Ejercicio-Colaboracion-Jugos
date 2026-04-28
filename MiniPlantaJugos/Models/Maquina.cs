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

        [Display(Name = "Estado")]
        public EstadoMaquina Estado { get; set; } = EstadoMaquina.Operativa;

        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        // Navegación
        public ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
        public ICollection<Incidente> Incidentes { get; set; } = new List<Incidente>();
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
