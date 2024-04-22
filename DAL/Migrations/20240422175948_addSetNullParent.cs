using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addSetNullParent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDay",
                value: new DateTime(2024, 4, 22, 17, 59, 47, 900, DateTimeKind.Utc).AddTicks(1293));

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2,
                column: "BirthDay",
                value: new DateTime(2024, 4, 22, 17, 59, 47, 900, DateTimeKind.Utc).AddTicks(1296));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDay",
                value: new DateTime(2024, 4, 22, 17, 21, 35, 273, DateTimeKind.Utc).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2,
                column: "BirthDay",
                value: new DateTime(2024, 4, 22, 17, 21, 35, 273, DateTimeKind.Utc).AddTicks(5208));
        }
    }
}
