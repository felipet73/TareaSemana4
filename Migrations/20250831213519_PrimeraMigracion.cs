using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareaSemana4.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula_RUC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo_Barras = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula_RUC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Create_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComprasCabecera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Codigo_Compra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sub_Total_Compra = table.Column<double>(type: "float", nullable: false),
                    Estado_Compra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descuento = table.Column<double>(type: "float", nullable: true),
                    Total_Compra = table.Column<double>(type: "float", nullable: false),
                    Metodo_Pago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorModelId = table.Column<int>(type: "int", nullable: false),
                    Create_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprasCabecera", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComprasCabecera_Clientes_ProveedorModelId",
                        column: x => x.ProveedorModelId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductosModelId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    ComprasCabeceraModelId = table.Column<int>(type: "int", nullable: false),
                    Create_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraDetalle_ComprasCabecera_ComprasCabeceraModelId",
                        column: x => x.ComprasCabeceraModelId,
                        principalTable: "ComprasCabecera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraDetalle_Productos_ProductosModelId",
                        column: x => x.ProductosModelId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_ComprasCabeceraModelId",
                table: "CompraDetalle",
                column: "ComprasCabeceraModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraDetalle_ProductosModelId",
                table: "CompraDetalle",
                column: "ProductosModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasCabecera_ProveedorModelId",
                table: "ComprasCabecera",
                column: "ProveedorModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraDetalle");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "ComprasCabecera");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
