using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddHotelContext03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 0, 46, 53, 372, DateTimeKind.Local).AddTicks(9280));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 0, 46, 53, 372, DateTimeKind.Local).AddTicks(9353));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 0, 46, 53, 372, DateTimeKind.Local).AddTicks(9358));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 0, 46, 53, 372, DateTimeKind.Local).AddTicks(9363));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 0, 29, 25, 963, DateTimeKind.Local).AddTicks(5464));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 0, 29, 25, 963, DateTimeKind.Local).AddTicks(5536));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 0, 29, 25, 963, DateTimeKind.Local).AddTicks(5541));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 18, 0, 29, 25, 963, DateTimeKind.Local).AddTicks(5546));
        }
    }
}
