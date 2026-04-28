using System.ComponentModel.DataAnnotations;
using MiniPlantaJugos.Enums;

namespace MiniPlantaJugos.Models
{
    public class Orden
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }

        [Display(Name = "Máquina")]
        public int MaquinaId { get; set; }
        public Maquina? Maquina { get; set; }

        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Display(Name = "Cantidad de Unidades")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int CantidadUnidades { get; set; }

        [Display(Name = "Fecha de Producción")]
        [DataType(DataType.Date)]
        public DateTime FechaProduccion { get; set; }

        [Display(Name = "Estado")]
        public EstadoOrden Estado { get; set; } = EstadoOrden.Pendiente;

        // Navegación
        public ICollection<ControlCalidad> ControlesCalidad { get; set; } = new List<ControlCalidad>();
    }
}
