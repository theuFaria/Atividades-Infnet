using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RotaCerta.Migrations
{
    /// <inheritdoc />
    public partial class InitialCorreta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinoPacoteTuristico");

            migrationBuilder.AddColumn<int>(
                name: "DestinoId",
                table: "PacoteTuristicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PacoteTuristicos_DestinoId",
                table: "PacoteTuristicos",
                column: "DestinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PacoteTuristicos_Destinos_DestinoId",
                table: "PacoteTuristicos",
                column: "DestinoId",
                principalTable: "Destinos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacoteTuristicos_Destinos_DestinoId",
                table: "PacoteTuristicos");

            migrationBuilder.DropIndex(
                name: "IX_PacoteTuristicos_DestinoId",
                table: "PacoteTuristicos");

            migrationBuilder.DropColumn(
                name: "DestinoId",
                table: "PacoteTuristicos");

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

            migrationBuilder.CreateIndex(
                name: "IX_DestinoPacoteTuristico_PacotesId",
                table: "DestinoPacoteTuristico",
                column: "PacotesId");
        }
    }
}
