using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RotaCerta.Migrations
{
    /// <inheritdoc />
    public partial class AddCampoNomeInReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Reservas",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Reservas");
        }
    }
}
