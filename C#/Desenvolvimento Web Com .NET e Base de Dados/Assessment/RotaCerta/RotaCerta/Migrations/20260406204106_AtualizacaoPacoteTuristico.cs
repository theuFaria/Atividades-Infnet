using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RotaCerta.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoPacoteTuristico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapacidadeDisponivel",
                table: "PacoteTuristicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadeDisponivel",
                table: "PacoteTuristicos");
        }
    }
}
