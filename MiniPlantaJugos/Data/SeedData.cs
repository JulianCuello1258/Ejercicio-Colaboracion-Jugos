using MiniPlantaJugos.Enums;
using MiniPlantaJugos.Models;

namespace MiniPlantaJugos.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Productos.Any()) return; // Ya hay datos

            // Productos
            Producto producto1 = new Producto { Nombre = "Jugo de Naranja", TipoEnvase = "Botella 500ml", PrecioEstimado = 85.00m, Activo = true };
            Producto producto2 = new Producto { Nombre = "Jugo de Manzana", TipoEnvase = "Botella 1L", PrecioEstimado = 120.00m, Activo = true };
            Producto producto3 = new Producto { Nombre = "Jugo de Uva", TipoEnvase = "Tetra Pak 200ml", PrecioEstimado = 50.00m, Activo = false };
            context.Productos.AddRange(producto1, producto2, producto3);
            context.SaveChanges();

            // Máquinas
            Maquina maquina1 = new Maquina { Nombre = "Envasadora 1", Sector = "Envasado", Estado = EstadoMaquina.Operativa, Precio = 50000m };
            Maquina maquina2 = new Maquina { Nombre = "Mezcladora Central", Sector = "Preparación", Estado = EstadoMaquina.Operativa, Precio = 75000m };
            Maquina maquina3 = new Maquina { Nombre = "Etiquetadora A", Sector = "Terminado", Estado = EstadoMaquina.Detenida, Precio = 30000m };
            context.Maquinas.AddRange(maquina1, maquina2, maquina3);
            context.SaveChanges();
        }
    }
}
