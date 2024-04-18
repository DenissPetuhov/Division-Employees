using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SetMistake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "Division",
                newName: "Description");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDay",
                value: new DateTime(2024, 4, 18, 20, 41, 41, 872, DateTimeKind.Utc).AddTicks(5412));

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2,
                column: "BirthDay",
                value: new DateTime(2024, 4, 18, 20, 41, 41, 872, DateTimeKind.Utc).AddTicks(5416));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Division",
                newName: "Discription");

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDay",
                value: new DateTime(2024, 4, 17, 13, 44, 12, 157, DateTimeKind.Utc).AddTicks(6483));

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2,
                column: "BirthDay",
                value: new DateTime(2024, 4, 17, 13, 44, 12, 157, DateTimeKind.Utc).AddTicks(6485));
        }
    }
}
