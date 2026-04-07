using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RotaCerta.Migrations
{
    /// <inheritdoc />
    public partial class FixDatePacoteTuristico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "PacoteTuristicos",
                newName: "DataRetorno");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reservas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataIda",
                table: "PacoteTuristicos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataIda",
                table: "PacoteTuristicos");

            migrationBuilder.RenameColumn(
                name: "DataRetorno",
                table: "PacoteTuristicos",
                newName: "DataInicio");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
