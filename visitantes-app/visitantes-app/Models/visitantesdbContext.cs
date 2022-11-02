using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace visitantes_app.Models
{
    public partial class visitantesdbContext : DbContext
    {
        public visitantesdbContext()
        {
        }

        public visitantesdbContext(DbContextOptions<visitantesdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Evento> Eventos { get; set; } = null!;
        public virtual DbSet<Historial> Historials { get; set; } = null!;
        public virtual DbSet<Historico> Historicos { get; set; } = null!;
        public virtual DbSet<Visitante> Visitantes { get; set; } = null!;
        public virtual DbSet<VisitanteEvento> VisitanteEventos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\SPICY;Database=visitantesdb;User ID=SPICY-SAUCE\\eliam;Trusted_Connection=true;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.ToTable("eventos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

            });

            modelBuilder.Entity<Historial>(entity =>
            {
                entity.ToTable("historial");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_salida");

                entity.Property(e => e.VisitanteId).HasColumnName("visitante_id");
            });

            modelBuilder.Entity<Historico>(entity =>
            {
                entity.ToTable("historico");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.EventoId).HasColumnName("evento_id");

                entity.Property(e => e.FechaModif)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modif");

                entity.Property(e => e.HistorialId).HasColumnName("historial_id");

                entity.Property(e => e.VisitanteId).HasColumnName("visitante_id");
            });

            modelBuilder.Entity<Visitante>(entity =>
            {
                entity.ToTable("visitantes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(80)
                    .HasColumnName("apellido");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(11)
                    .HasColumnName("cedula");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(80)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<VisitanteEvento>(entity =>
            {
                entity.ToTable("visitanteEventos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EventoId).HasColumnName("evento_id");

                entity.Property(e => e.VisitanteId).HasColumnName("visitante_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
