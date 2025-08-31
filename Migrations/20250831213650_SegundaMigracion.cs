using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareaSemana4.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VentasCabecera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Codigo_Venta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sub_Total_Venta = table.Column<double>(type: "float", nullable: false),
                    Estado_Venta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descuento = table.Column<double>(type: "float", nullable: true),
                    Total_Venta = table.Column<double>(type: "float", nullable: false),
                    Metodo_Pago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientesModelId = table.Column<int>(type: "int", nullable: false),
                    Create_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasCabecera", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentasCabecera_Clientes_ClientesModelId",
                        column: x => x.ClientesModelId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentasDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductosModelId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    VentasCabeceraModelId = table.Column<int>(type: "int", nullable: false),
                    Create_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentasDetalle_Productos_ProductosModelId",
                        column: x => x.ProductosModelId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VentasDetalle_VentasCabecera_VentasCabeceraModelId",
                        column: x => x.VentasCabeceraModelId,
                        principalTable: "VentasCabecera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VentasCabecera_ClientesModelId",
                table: "VentasCabecera",
                column: "ClientesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VentasDetalle_ProductosModelId",
                table: "VentasDetalle",
                column: "ProductosModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VentasDetalle_VentasCabeceraModelId",
                table: "VentasDetalle",
                column: "VentasCabeceraModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "VentasDetalle");

            migrationBuilder.DropTable(
                name: "VentasCabecera");
        }
    }
}
