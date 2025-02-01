using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecusrosUD_API.Migrations
{
    /// <inheritdoc />
    public partial class actuRecursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "horario_fin",
                table: "recursos");

            migrationBuilder.DropColumn(
                name: "horario_inicio",
                table: "recursos");

            migrationBuilder.AddColumn<long>(
                name: "nombre",
                table: "recursos",
                type: "bigint",
                maxLength: 255,
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nombre",
                table: "recursos");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "horario_fin",
                table: "recursos",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "horario_inicio",
                table: "recursos",
                type: "time",
                nullable: true);
        }
    }
}
