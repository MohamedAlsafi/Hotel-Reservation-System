using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddHotelContext02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 23, 1, 28, 337, DateTimeKind.Local).AddTicks(1492));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 23, 1, 28, 337, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 23, 1, 28, 337, DateTimeKind.Local).AddTicks(1566));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 17, 23, 1, 28, 337, DateTimeKind.Local).AddTicks(1571));
        }
    }
}
