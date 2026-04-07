using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RotaCerta.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destinos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    PaisDestino = table.Column<string>(type: "TEXT", nullable: false),
                    CidadeDestino = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PacoteTuristicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CapacidadeMaxima = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacoteTuristicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinoPacoteTuristico",
                columns: table => new
                {
                    DestinosId = table.Column<int>(type: "INTEGER", nullable: false),
                    PacotesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinoPacoteTuristico", x => new { x.DestinosId, x.PacotesId });
                    table.ForeignKey(
                        name: "FK_DestinoPacoteTuristico_Destinos_DestinosId",
                        column: x => x.DestinosId,
                        principalTable: "Destinos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DestinoPacoteTuristico_PacoteTuristicos_PacotesId",
                        column: x => x.PacotesId,
                        principalTable: "PacoteTuristicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    PacoteTuristicoId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataReserva = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_PacoteTuristicos_PacoteTuristicoId",
                        column: x => x.PacoteTuristicoId,
                        principalTable: "PacoteTuristicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Email", "Nome", "Senha" },
                values: new object[] { 1, "mdsfaria@gmail.com", "Matheus Faria", "123456" });

            migrationBuilder.InsertData(
                table: "Destinos",
                columns: new[] { "Id", "CidadeDestino", "Nome", "PaisDestino" },
                values: new object[,]
                {
                    { 1, "Las Vegas", "Las Vegas", "Estados Unidos" },
                    { 2, "Orlando", "Disney Orlando", "Estados Unidos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DestinoPacoteTuristico_PacotesId",
                table: "DestinoPacoteTuristico",
                column: "PacotesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteId_PacoteTuristicoId_DataReserva",
                table: "Reservas",
                columns: new[] { "ClienteId", "PacoteTuristicoId", "DataReserva" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_PacoteTuristicoId",
                table: "Reservas",
                column: "PacoteTuristicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinoPacoteTuristico");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Destinos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "PacoteTuristicos");
        }
    }
}
