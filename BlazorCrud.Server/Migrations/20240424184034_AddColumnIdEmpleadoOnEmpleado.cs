using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorCrud.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnIdEmpleadoOnEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departamento",
                columns: table => new
                {
                    idDepartamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__departam__C225F98D21C47132", x => x.idDepartamento);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdDepartamentoNavigationIdDepartamento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleado_departamento_IdDepartamentoNavigationIdDepartamento",
                        column: x => x.IdDepartamentoNavigationIdDepartamento,
                        principalTable: "departamento",
                        principalColumn: "idDepartamento");
                });

            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    idPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreCompleto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idDepartamento = table.Column<int>(type: "int", nullable: true),
                    sueldo = table.Column<int>(type: "int", nullable: true),
                    fechaContrato = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__persona__A47881413623E1E7", x => x.idPersona);
                    table.ForeignKey(
                        name: "FK__persona__idDepar__25869641",
                        column: x => x.idDepartamento,
                        principalTable: "departamento",
                        principalColumn: "idDepartamento");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdDepartamentoNavigationIdDepartamento",
                table: "Empleado",
                column: "IdDepartamentoNavigationIdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_persona_idDepartamento",
                table: "persona",
                column: "idDepartamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "persona");

            migrationBuilder.DropTable(
                name: "departamento");
        }
    }
}
