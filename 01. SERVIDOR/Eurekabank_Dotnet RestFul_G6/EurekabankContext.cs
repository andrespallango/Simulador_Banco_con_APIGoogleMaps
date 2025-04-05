using Microsoft.EntityFrameworkCore;
using Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.modelo;

namespace Eurekabank_Dotnet_RestFul_G6.Models
{
    public class EurekabankContext : DbContext
    {
        public EurekabankContext(DbContextOptions<EurekabankContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoMovimiento> TipoMovimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CliCodigo);
                entity.Property(e => e.CliNombre).IsRequired().HasMaxLength(30);
                entity.Property(e => e.CliApellido).IsRequired().HasMaxLength(25);
                entity.Property(e => e.CliCorreo).HasMaxLength(50);
                entity.Property(e => e.CliTelefono).HasMaxLength(20);
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.ToTable("cuenta");
                entity.HasKey(e => e.chr_cuencodigo);
                entity.Property(e => e.dec_cuensaldo).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(e => e.int_cuencontmov).IsRequired();
                entity.Property(e => e.vch_cuenestado).IsRequired().HasMaxLength(15);
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.ToTable("movimiento");
                entity.HasKey(e => new { e.chr_cuencodigo, e.int_movinumero });
                entity.Property(e => e.chr_cuencodigo).IsRequired().HasMaxLength(8);
                entity.Property(e => e.dtt_movifecha).IsRequired();
                entity.Property(e => e.chr_emplcodigo).IsRequired().HasMaxLength(4);
                entity.Property(e => e.chr_tipocodigo).IsRequired().HasMaxLength(3);
                entity.Property(e => e.dec_moviimporte).IsRequired().HasColumnType("decimal(12,2)");
                entity.Property(e => e.chr_cuenreferencia).HasMaxLength(8);
            });

            modelBuilder.Entity<TipoMovimiento>(entity =>
            {
                entity.ToTable("tipomovimiento");
                entity.HasKey(e => e.chr_tipocodigo);
                entity.Property(e => e.vch_tipodescripcion).HasMaxLength(40);
                entity.Property(e => e.vch_tipoaccion).HasMaxLength(10);
                entity.Property(e => e.vch_tipoestado).HasMaxLength(15);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Username);
                entity.Property(e => e.PasswordHash).IsRequired();
            });
        }
    }
}