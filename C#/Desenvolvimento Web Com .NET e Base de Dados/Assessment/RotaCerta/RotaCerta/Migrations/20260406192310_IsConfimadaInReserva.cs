using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RotaCerta.Migrations
{
    /// <inheritdoc />
    public partial class IsConfimadaInReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmada",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmada",
                table: "Reservas");
        }
    }
}
