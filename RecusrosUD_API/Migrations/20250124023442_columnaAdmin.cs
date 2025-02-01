using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecusrosUD_API.Migrations
{
    /// <inheritdoc />
    public partial class columnaAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "es_empleado",
                table: "usuarios",
                newName: "admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "admin",
                table: "usuarios",
                newName: "es_empleado");
        }
    }
}
