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

            // Usuarios
            Usuario usuario1 = new Usuario { Nombre = "Carlos López", Cargo = Cargo.JefePlanta };
            Usuario usuario2 = new Usuario { Nombre = "María García", Cargo = Cargo.Operario };
            Usuario usuario3 = new Usuario { Nombre = "Juan Pérez", Cargo = Cargo.JefeMantenimiento };
            context.Usuarios.AddRange(usuario1, usuario2, usuario3);
            context.SaveChanges();

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

            // Asignar usuarios a máquinas
            maquina1.Usuarios.Add(usuario2);
            maquina2.Usuarios.Add(usuario2);
            maquina3.Usuarios.Add(usuario3);
            context.SaveChanges();

            // Órdenes de producción
            Orden orden1 = new Orden
            {
                ProductoId = producto1.Id,
                MaquinaId = maquina1.Id,
                UsuarioId = usuario1.Id,
                CantidadUnidades = 300,
                FechaProduccion = DateTime.Now.AddDays(-2),
                Estado = EstadoOrden.Finalizada
            };
            Orden orden2 = new Orden
            {
                ProductoId = producto2.Id,
                MaquinaId = maquina2.Id,
                UsuarioId = usuario1.Id,
                CantidadUnidades = 150,
                FechaProduccion = DateTime.Now,
                Estado = EstadoOrden.Pendiente
            };
            context.Ordenes.AddRange(orden1, orden2);
            context.SaveChanges();

            // Control de calidad
            ControlCalidad control1 = new ControlCalidad
            {
                OrdenId = orden1.Id,
                Resultado = ResultadoCalidad.Observado,
                Observacion = "Algunas botellas tienen etiquetas mal colocadas",
                FechaControl = DateTime.Now.AddDays(-1)
            };
            context.ControlesCalidad.Add(control1);
            context.SaveChanges();

            // Incidente
            Incidente incidente1 = new Incidente
            {
                MaquinaId = maquina3.Id,
                UsuarioId = usuario3.Id,
                Descripcion = "Falla en el sistema de impresión de etiquetas",
                Severidad = Severidad.Media,
                Estado = EstadoIncidente.Abierto
            };
            context.Incidentes.Add(incidente1);
            context.SaveChanges();
        }
    }
}
