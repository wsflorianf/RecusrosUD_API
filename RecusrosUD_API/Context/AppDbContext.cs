using Microsoft.EntityFrameworkCore;
using RecusrosUD_API.Models;

namespace RecusrosUD_API.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificacion> Calificaciones { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Recurso> Recursos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<TipoRecurso> TiposRecursos { get; set; }

    public virtual DbSet<UnidadServicio> UnidadesServicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;database=recursos_ud;user=admin;password=root", ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("calificaciones");

            entity.HasIndex(e => e.ReservaId, "reserva_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmabilidadPersonal).HasColumnName("amabilidad_personal");
            entity.Property(e => e.CalidadEstadoRecurso).HasColumnName("calidad_estado_recurso");
            entity.Property(e => e.CumplimientoHorarios).HasColumnName("cumplimiento_horarios");
            entity.Property(e => e.ReservaId).HasColumnName("reserva_id");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.ReservaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calificaciones_ibfk_1");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("prestamos");

            entity.HasIndex(e => e.EmpleadoEntregaId, "empleado_entrega_id");

            entity.HasIndex(e => e.EmpleadoRecepcionId, "empleado_recepcion_id");

            entity.HasIndex(e => e.ReservaId, "reserva_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmpleadoEntregaId).HasColumnName("empleado_entrega_id");
            entity.Property(e => e.EmpleadoRecepcionId).HasColumnName("empleado_recepcion_id");
            entity.Property(e => e.HoraDevolucion)
                .HasColumnType("time")
                .HasColumnName("hora_devolucion");
            entity.Property(e => e.HoraEntrega)
                .HasColumnType("time")
                .HasColumnName("hora_entrega");
            entity.Property(e => e.ReservaId).HasColumnName("reserva_id");

            entity.HasOne(d => d.EmpleadoEntrega).WithMany(p => p.PrestamoEmpleadoEntregas)
                .HasForeignKey(d => d.EmpleadoEntregaId)
                .HasConstraintName("prestamos_ibfk_2");

            entity.HasOne(d => d.EmpleadoRecepcion).WithMany(p => p.PrestamoEmpleadoRecepcions)
                .HasForeignKey(d => d.EmpleadoRecepcionId)
                .HasConstraintName("prestamos_ibfk_3");

            entity.HasOne(d => d.Reserva).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.ReservaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestamos_ibfk_1");
        });

        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("recursos");

            entity.HasIndex(e => e.TipoRecursoId, "tipo_recurso_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasMaxLength(255).HasColumnName("nombre");
            entity.Property(e => e.Caracteristicas)
                .HasColumnType("text")
                .HasColumnName("caracteristicas");
            entity.Property(e => e.Identificador)
                .HasMaxLength(255)
                .HasColumnName("identificador");
            entity.Property(e => e.TipoRecursoId).HasColumnName("tipo_recurso_id");

            entity.HasOne(d => d.TipoRecurso).WithMany(p => p.Recursos)
                .HasForeignKey(d => d.TipoRecursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recursos_ibfk_1");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reservas");

            entity.HasIndex(e => e.RecursoId, "recurso_id");

            entity.HasIndex(e => e.UsuarioId, "usuario_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraFin)
                .HasColumnType("time")
                .HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio)
                .HasColumnType("time")
                .HasColumnName("hora_inicio");
            entity.Property(e => e.RecursoId).HasColumnName("recurso_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Recurso).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.RecursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservas_ibfk_2");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservas_ibfk_1");
        });

        modelBuilder.Entity<TipoRecurso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipos_recursos");

            entity.HasIndex(e => e.UnidadId, "unidad_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Caracteristicas)
                .HasColumnType("text")
                .HasColumnName("caracteristicas");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.HorarioDisponibilidad)
                .HasColumnType("json")
                .HasColumnName("horario_disponibilidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.UnidadId).HasColumnName("unidad_id");

            entity.HasOne(d => d.Unidad).WithMany(p => p.TiposRecursos)
                .HasForeignKey(d => d.UnidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tipos_recursos_ibfk_1");
        });

        modelBuilder.Entity<UnidadServicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("unidades_servicio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HorarioDisponibilidad)
                .HasColumnType("json")
                .HasColumnName("horario_disponibilidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.TiempoMin)
                .HasColumnType("time")
                .HasColumnName("tiempo_min");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contra)
                .HasMaxLength(255)
                .HasColumnName("contra");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.HasIndex(e => e.Correo).IsUnique();
            entity.Property(e => e.Admin).HasColumnName("admin");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
