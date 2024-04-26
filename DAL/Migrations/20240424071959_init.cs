using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ParentDivisionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Division_Division_ParentDivisionId",
                        column: x => x.ParentDivisionId,
                        principalTable: "Division",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverLicense = table.Column<bool>(type: "bit", nullable: true),
                    DivisionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Division_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Division",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Division",
                columns: new[] { "Id", "DateCreate", "Description", "Name", "ParentDivisionId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maffia", "OPG#1", null });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "BirthDay", "DivisionId", "DriverLicense", "FirstName", "Gender", "LastName", "Position", "SecondName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 24, 7, 19, 58, 900, DateTimeKind.Utc).AddTicks(6843), 1, true, "Валерий", null, "Альбертович", "General", "Жмышенко" },
                    { 2, new DateTime(2024, 4, 24, 7, 19, 58, 900, DateTimeKind.Utc).AddTicks(6845), 1, true, "Михаил", null, "Петрович", "Maffiosnic", "Зубенко" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Division_ParentDivisionId",
                table: "Division",
                column: "ParentDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DivisionId",
                table: "Employee",
                column: "DivisionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Division");
        }
    }
}
