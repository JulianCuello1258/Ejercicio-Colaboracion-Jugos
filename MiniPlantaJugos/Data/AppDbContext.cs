using Microsoft.EntityFrameworkCore;
using MiniPlantaJugos.Models;

namespace MiniPlantaJugos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<ControlCalidad> ControlesCalidad { get; set; }
        public DbSet<Incidente> Incidentes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Orden -> Producto
            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Producto)
                .WithMany(p => p.Ordenes)
                .HasForeignKey(o => o.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Orden -> Maquina
            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Maquina)
                .WithMany(m => m.Ordenes)
                .HasForeignKey(o => o.MaquinaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Orden -> Usuario
            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Usuario)
                .WithMany(u => u.Ordenes)
                .HasForeignKey(o => o.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación ControlCalidad -> Orden
            modelBuilder.Entity<ControlCalidad>()
                .HasOne(c => c.Orden)
                .WithMany(o => o.ControlesCalidad)
                .HasForeignKey(c => c.OrdenId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Incidente -> Maquina
            modelBuilder.Entity<Incidente>()
                .HasOne(i => i.Maquina)
                .WithMany(m => m.Incidentes)
                .HasForeignKey(i => i.MaquinaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Incidente -> Usuario
            modelBuilder.Entity<Incidente>()
                .HasOne(i => i.Usuario)
                .WithMany(u => u.Incidentes)
                .HasForeignKey(i => i.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many: Usuario <-> Maquina
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Maquinas)
                .WithMany(m => m.Usuarios);

            // Conversiones de enum a string
            modelBuilder.Entity<Maquina>()
                .Property(m => m.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Orden>()
                .Property(o => o.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<ControlCalidad>()
                .Property(c => c.Resultado)
                .HasConversion<string>();

            modelBuilder.Entity<Incidente>()
                .Property(i => i.Severidad)
                .HasConversion<string>();

            modelBuilder.Entity<Incidente>()
                .Property(i => i.Estado)
                .HasConversion<string>();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Cargo)
                .HasConversion<string>();
        }
    }
}
