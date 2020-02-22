using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoPOA.Models
{
    public partial class itesrcne_poaContext : DbContext
    {
        public itesrcne_poaContext()
        {
        }

        public itesrcne_poaContext(DbContextOptions<itesrcne_poaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Unidadadministrativa> Unidadadministrativa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=itesrcne_poa;uid=root;password=root");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unidadadministrativa>(entity =>
            {
                entity.ToTable("unidadadministrativa");

                entity.HasIndex(e => e.IdUnidadSuperior)
                    .HasName("fkIdSuperiorId_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Clave).HasColumnType("int(4)");

                entity.Property(e => e.Eliminado)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Encargado)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IdUnidadSuperior).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdUnidadSuperiorNavigation)
                    .WithMany(p => p.InverseIdUnidadSuperiorNavigation)
                    .HasForeignKey(d => d.IdUnidadSuperior)
                    .HasConstraintName("fkIdSuperiorId");
            });
        }
    }
}
