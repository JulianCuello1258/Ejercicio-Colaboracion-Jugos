using Microsoft.EntityFrameworkCore;
using MiniPlantaJugos.Models;

namespace MiniPlantaJugos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<OrdenProd> OrdenesProd { get; set; }
        public DbSet<ControlCalidad> ControlesCalidad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Conversiones de enum a string
            modelBuilder.Entity<Maquina>()
                .Property(m => m.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<OrdenProd>()
                .Property(o => o.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<ControlCalidad>()
                .Property(c => c.Resultado)
                .HasConversion<string>();
        }
    }
}
