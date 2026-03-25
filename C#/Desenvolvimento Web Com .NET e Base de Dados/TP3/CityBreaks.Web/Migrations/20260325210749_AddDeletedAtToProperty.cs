using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityBreaks.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAtToProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Properety",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Properety",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Properety",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Properety",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeletedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Properety",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeletedAt",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Properety");
        }
    }
}
