using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RotaCerta.Migrations
{
    /// <inheritdoc />
    public partial class MaisDestinos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Destinos",
                columns: new[] { "Id", "CidadeDestino", "Nome", "PaisDestino" },
                values: new object[,]
                {
                    { 3, "Rio de Janeiro", "Rio de Janeiro", "Brasil" },
                    { 4, "Angra dos Reis", "Angra dos Reis", "Brasil" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Destinos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Destinos",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
