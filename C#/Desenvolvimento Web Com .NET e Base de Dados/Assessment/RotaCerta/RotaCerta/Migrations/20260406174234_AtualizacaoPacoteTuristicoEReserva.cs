using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RotaCerta.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoPacoteTuristicoEReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "PacoteTuristicos",
                newName: "PrecoPorPessoa");

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeDePessoas",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeDePessoas",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "PrecoPorPessoa",
                table: "PacoteTuristicos",
                newName: "Preco");
        }
    }
}
