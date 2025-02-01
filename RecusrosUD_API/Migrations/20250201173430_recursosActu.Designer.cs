﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecusrosUD_API.Context;

#nullable disable

namespace RecusrosUD_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250201173430_recursosActu")]
    partial class recursosActu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("RecusrosUD_API.Models.Calificacion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<int?>("AmabilidadPersonal")
                        .HasColumnType("int")
                        .HasColumnName("amabilidad_personal");

                    b.Property<int?>("CalidadEstadoRecurso")
                        .HasColumnType("int")
                        .HasColumnName("calidad_estado_recurso");

                    b.Property<int?>("CumplimientoHorarios")
                        .HasColumnType("int")
                        .HasColumnName("cumplimiento_horarios");

                    b.Property<long>("ReservaId")
                        .HasColumnType("bigint")
                        .HasColumnName("reserva_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "ReservaId" }, "reserva_id");

                    b.ToTable("calificaciones", (string)null);
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Efmigrationshistory", b =>
                {
                    b.Property<string>("MigrationId")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ProductVersion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("MigrationId")
                        .HasName("PRIMARY");

                    b.ToTable("__efmigrationshistory", (string)null);
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Prestamo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("EmpleadoEntregaId")
                        .HasColumnType("bigint")
                        .HasColumnName("empleado_entrega_id");

                    b.Property<long?>("EmpleadoRecepcionId")
                        .HasColumnType("bigint")
                        .HasColumnName("empleado_recepcion_id");

                    b.Property<TimeOnly?>("HoraDevolucion")
                        .HasColumnType("time")
                        .HasColumnName("hora_devolucion");

                    b.Property<TimeOnly?>("HoraEntrega")
                        .HasColumnType("time")
                        .HasColumnName("hora_entrega");

                    b.Property<long>("ReservaId")
                        .HasColumnType("bigint")
                        .HasColumnName("reserva_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "EmpleadoEntregaId" }, "empleado_entrega_id");

                    b.HasIndex(new[] { "EmpleadoRecepcionId" }, "empleado_recepcion_id");

                    b.HasIndex(new[] { "ReservaId" }, "reserva_id")
                        .HasDatabaseName("reserva_id1");

                    b.ToTable("prestamos", (string)null);
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Recurso", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Caracteristicas")
                        .HasColumnType("text")
                        .HasColumnName("caracteristicas");

                    b.Property<string>("Identificador")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("identificador");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nombre");

                    b.Property<long>("TipoRecursoId")
                        .HasColumnType("bigint")
                        .HasColumnName("tipo_recurso_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "TipoRecursoId" }, "tipo_recurso_id");

                    b.ToTable("recursos", (string)null);
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Reserva", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("Fecha")
                        .HasColumnType("date")
                        .HasColumnName("fecha");

                    b.Property<TimeOnly>("HoraFin")
                        .HasColumnType("time")
                        .HasColumnName("hora_fin");

                    b.Property<TimeOnly>("HoraInicio")
                        .HasColumnType("time")
                        .HasColumnName("hora_inicio");

                    b.Property<long>("RecursoId")
                        .HasColumnType("bigint")
                        .HasColumnName("recurso_id");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint")
                        .HasColumnName("usuario_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "RecursoId" }, "recurso_id");

                    b.HasIndex(new[] { "UsuarioId" }, "usuario_id");

                    b.ToTable("reservas", (string)null);
                });

            modelBuilder.Entity("RecusrosUD_API.Models.TipoRecurso", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Caracteristicas")
                        .HasColumnType("text")
                        .HasColumnName("caracteristicas");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text")
                        .HasColumnName("descripcion");

                    b.Property<string>("HorarioDisponibilidad")
                        .IsRequired()
                        .HasColumnType("json")
                        .HasColumnName("horario_disponibilidad");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nombre");

                    b.Property<long>("UnidadId")
                        .HasColumnType("bigint")
                        .HasColumnName("unidad_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "UnidadId" }, "unidad_id");

                    b.ToTable("tipos_recursos", (string)null);
                });

            modelBuilder.Entity("RecusrosUD_API.Models.UnidadServicio", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("HorarioDisponibilidad")
                        .IsRequired()
                        .HasColumnType("json")
                        .HasColumnName("horario_disponibilidad");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nombre");

                    b.Property<TimeOnly>("TiempoMin")
                        .HasColumnType("time")
                        .HasColumnName("tiempo_min");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("unidades_servicio", (string)null);
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Admin")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("admin");

                    b.Property<string>("Contra")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("contra");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("correo");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nombre");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex("Correo")
                        .IsUnique();

                    b.ToTable("usuarios", (string)null);
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Calificacion", b =>
                {
                    b.HasOne("RecusrosUD_API.Models.Reserva", "Reserva")
                        .WithMany("Calificaciones")
                        .HasForeignKey("ReservaId")
                        .IsRequired()
                        .HasConstraintName("calificaciones_ibfk_1");

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Prestamo", b =>
                {
                    b.HasOne("RecusrosUD_API.Models.Usuario", "EmpleadoEntrega")
                        .WithMany("PrestamoEmpleadoEntregas")
                        .HasForeignKey("EmpleadoEntregaId")
                        .HasConstraintName("prestamos_ibfk_2");

                    b.HasOne("RecusrosUD_API.Models.Usuario", "EmpleadoRecepcion")
                        .WithMany("PrestamoEmpleadoRecepcions")
                        .HasForeignKey("EmpleadoRecepcionId")
                        .HasConstraintName("prestamos_ibfk_3");

                    b.HasOne("RecusrosUD_API.Models.Reserva", "Reserva")
                        .WithMany("Prestamos")
                        .HasForeignKey("ReservaId")
                        .IsRequired()
                        .HasConstraintName("prestamos_ibfk_1");

                    b.Navigation("EmpleadoEntrega");

                    b.Navigation("EmpleadoRecepcion");

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Recurso", b =>
                {
                    b.HasOne("RecusrosUD_API.Models.TipoRecurso", "TipoRecurso")
                        .WithMany("Recursos")
                        .HasForeignKey("TipoRecursoId")
                        .IsRequired()
                        .HasConstraintName("recursos_ibfk_1");

                    b.Navigation("TipoRecurso");
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Reserva", b =>
                {
                    b.HasOne("RecusrosUD_API.Models.Recurso", "Recurso")
                        .WithMany("Reservas")
                        .HasForeignKey("RecursoId")
                        .IsRequired()
                        .HasConstraintName("reservas_ibfk_2");

                    b.HasOne("RecusrosUD_API.Models.Usuario", "Usuario")
                        .WithMany("Reservas")
                        .HasForeignKey("UsuarioId")
                        .IsRequired()
                        .HasConstraintName("reservas_ibfk_1");

                    b.Navigation("Recurso");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RecusrosUD_API.Models.TipoRecurso", b =>
                {
                    b.HasOne("RecusrosUD_API.Models.UnidadServicio", "Unidad")
                        .WithMany("TiposRecursos")
                        .HasForeignKey("UnidadId")
                        .IsRequired()
                        .HasConstraintName("tipos_recursos_ibfk_1");

                    b.Navigation("Unidad");
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Recurso", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Reserva", b =>
                {
                    b.Navigation("Calificaciones");

                    b.Navigation("Prestamos");
                });

            modelBuilder.Entity("RecusrosUD_API.Models.TipoRecurso", b =>
                {
                    b.Navigation("Recursos");
                });

            modelBuilder.Entity("RecusrosUD_API.Models.UnidadServicio", b =>
                {
                    b.Navigation("TiposRecursos");
                });

            modelBuilder.Entity("RecusrosUD_API.Models.Usuario", b =>
                {
                    b.Navigation("PrestamoEmpleadoEntregas");

                    b.Navigation("PrestamoEmpleadoRecepcions");

                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
