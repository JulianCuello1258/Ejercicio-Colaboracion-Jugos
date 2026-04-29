using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MiniPlantaJugos.Enums;

namespace MiniPlantaJugos.Models
{
    public class OrdenProd
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un producto")]
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una máquina")]
        [Display(Name = "Máquina")]
        public int MaquinaId { get; set; }

        [ForeignKey("MaquinaId")]
        public Maquina? Maquina { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad de unidades")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        [Display(Name = "Cantidad de Unidades")]
        public int CantUnidades { get; set; }

        [Required(ErrorMessage = "Debe seleccionar la fecha de producción")]
        [Display(Name = "Fecha de Producción")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [Display(Name = "Estado")]
        public EstadoOrden Estado { get; set; } = EstadoOrden.Pendiente;

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(100, ErrorMessage = "El nombre de usuario no puede exceder los 100 caracteres")]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; } = string.Empty;
    }
}
