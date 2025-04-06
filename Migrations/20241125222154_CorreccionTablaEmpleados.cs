using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionTablaEmpleados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Documento",
                table: "Empleados",
                column: "Documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Telefono",
                table: "Empleados",
                column: "Telefono",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_Documento",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_Telefono",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Empleados");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados",
                column: "Documento");
        }
    }
}
