using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class Yaraaab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 1, 18, 13, 703, DateTimeKind.Local).AddTicks(5193));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 1, 18, 13, 703, DateTimeKind.Local).AddTicks(5271));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 1, 18, 13, 703, DateTimeKind.Local).AddTicks(5276));

            migrationBuilder.UpdateData(
                table: "Facility",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 15, 1, 18, 13, 703, DateTimeKind.Local).AddTicks(5280));
        }
    }
}
