using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class YarabMaYedrabException : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerData", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 16, 14, 33, 37, 632, DateTimeKind.Local).AddTicks(766));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 16, 14, 33, 37, 632, DateTimeKind.Local).AddTicks(830));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 16, 14, 33, 37, 632, DateTimeKind.Local).AddTicks(834));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 16, 14, 33, 37, 632, DateTimeKind.Local).AddTicks(839));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerData");

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 17, 28, 52, 560, DateTimeKind.Local).AddTicks(7827));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 17, 28, 52, 560, DateTimeKind.Local).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 17, 28, 52, 560, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 17, 28, 52, 560, DateTimeKind.Local).AddTicks(7882));
        }
    }
}
