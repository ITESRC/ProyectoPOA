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

        public virtual DbSet<Articulo> Articulo { get; set; }
        public virtual DbSet<Capitulo> Capitulo { get; set; }
        public virtual DbSet<Estrategia> Estrategia { get; set; }
        public virtual DbSet<Objetivo> Objetivo { get; set; }
        public virtual DbSet<Partida> Partida { get; set; }
        public virtual DbSet<Uaestrategia> Uaestrategia { get; set; }
        public virtual DbSet<Uapartidas> Uapartidas { get; set; }
        public virtual DbSet<Unidadadministrativa> Unidadadministrativa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=204.93.216.11;port=3306;database=itesrcne_poa;uid=itesrcne_poa;password=proyectopoa20");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>(entity =>
            {
                entity.HasKey(e => e.IdArticulo);

                entity.ToTable("articulo");

                entity.HasIndex(e => e.Idpartida)
                    .HasName("fkPartidaArticulo_idx");

                entity.Property(e => e.IdArticulo)
                    .HasColumnName("idArticulo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CostoUnitario)
                    .HasColumnName("costo unitario")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Eliminado)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Idpartida)
                    .HasColumnName("idpartida")
                    .HasColumnType("smallint(4)");

                entity.Property(e => e.UnidadDeMedida)
                    .IsRequired()
                    .HasColumnName("unidad de medida")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.IdpartidaNavigation)
                    .WithMany(p => p.Articulo)
                    .HasForeignKey(d => d.Idpartida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkPartidaArticulo");
            });

            modelBuilder.Entity<Capitulo>(entity =>
            {
                entity.ToTable("capitulo");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Clave).HasColumnType("smallint(5)");

                entity.Property(e => e.Eliminado)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Estrategia>(entity =>
            {
                entity.ToTable("estrategia");

                entity.HasIndex(e => e.IdObjetivo)
                    .HasName("fkIdObjetivo_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Eliminado)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.IdObjetivo).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(140)");

                entity.HasOne(d => d.IdObjetivoNavigation)
                    .WithMany(p => p.Estrategia)
                    .HasForeignKey(d => d.IdObjetivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkIdObjetivo");
            });

            modelBuilder.Entity<Objetivo>(entity =>
            {
                entity.ToTable("objetivo");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Eliminado)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(80)");
            });

            modelBuilder.Entity<Partida>(entity =>
            {
                entity.HasKey(e => e.Clave);

                entity.ToTable("partida");

                entity.HasIndex(e => e.Capitulo)
                    .HasName("fkpartidacapitulo_idx");

                entity.Property(e => e.Clave).HasColumnType("smallint(4)");

                entity.Property(e => e.Capitulo).HasColumnType("int(11)");

                entity.Property(e => e.Concepto)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Eliminado)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.HasOne(d => d.CapituloNavigation)
                    .WithMany(p => p.Partida)
                    .HasForeignKey(d => d.Capitulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkpartidacapitulo");
            });

            modelBuilder.Entity<Uaestrategia>(entity =>
            {
                entity.ToTable("uaestrategia");

                entity.HasIndex(e => e.IdEstrategia)
                    .HasName("fkEstrategia_idx");

                entity.HasIndex(e => e.IdUa)
                    .HasName("fkua_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdEstrategia).HasColumnType("int(11)");

                entity.Property(e => e.IdUa).HasColumnType("int(11)");

                entity.HasOne(d => d.IdEstrategiaNavigation)
                    .WithMany(p => p.Uaestrategia)
                    .HasForeignKey(d => d.IdEstrategia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkEstrategia");

                entity.HasOne(d => d.IdUaNavigation)
                    .WithMany(p => p.Uaestrategia)
                    .HasForeignKey(d => d.IdUa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkua");
            });

            modelBuilder.Entity<Uapartidas>(entity =>
            {
                entity.ToTable("uapartidas");

                entity.HasIndex(e => e.IdPartida)
                    .HasName("fkupPartidas_idx");

                entity.HasIndex(e => e.IdUa)
                    .HasName("fkUA_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdPartida).HasColumnType("smallint(6)");

                entity.Property(e => e.IdUa)
                    .HasColumnName("IdUA")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdPartidaNavigation)
                    .WithMany(p => p.Uapartidas)
                    .HasForeignKey(d => d.IdPartida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkupPartidas");

                entity.HasOne(d => d.IdUaNavigation)
                    .WithMany(p => p.Uapartidas)
                    .HasForeignKey(d => d.IdUa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkupUA");
            });

            modelBuilder.Entity<Unidadadministrativa>(entity =>
            {
                entity.ToTable("unidadadministrativa");

                entity.HasIndex(e => e.IdUnidadSuperior)
                    .HasName("fkIdSuperiorId_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Clave).HasColumnType("smallint(4)");

                entity.Property(e => e.Eliminado)
                    .IsRequired()
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.Encargado)
                    .IsRequired()
                    .HasColumnType("varchar(60)");

                entity.Property(e => e.IdUnidadSuperior).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(60)");

                entity.HasOne(d => d.IdUnidadSuperiorNavigation)
                    .WithMany(p => p.InverseIdUnidadSuperiorNavigation)
                    .HasForeignKey(d => d.IdUnidadSuperior)
                    .HasConstraintName("fkIdSuperiorId");
            });
        }
    }
}
