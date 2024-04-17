using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Redact_DriverLicense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DriverLicense",
                table: "Employee",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDay", "DriverLicense" },
                values: new object[] { new DateTime(2024, 4, 17, 13, 44, 12, 157, DateTimeKind.Utc).AddTicks(6483), true });

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDay", "DriverLicense" },
                values: new object[] { new DateTime(2024, 4, 17, 13, 44, 12, 157, DateTimeKind.Utc).AddTicks(6485), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DriverLicense",
                table: "Employee",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDay", "DriverLicense" },
                values: new object[] { new DateTime(2024, 4, 17, 13, 38, 32, 447, DateTimeKind.Utc).AddTicks(9524), 1 });

            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDay", "DriverLicense" },
                values: new object[] { new DateTime(2024, 4, 17, 13, 38, 32, 447, DateTimeKind.Utc).AddTicks(9528), 1 });
        }
    }
}
