using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Core.Migrations.HotelDb
{
    /// <inheritdoc />
    public partial class RemoveIsAvailableFromRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentStatus",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 13, 3, 10, 17, 794, DateTimeKind.Local).AddTicks(8047));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 13, 3, 10, 17, 794, DateTimeKind.Local).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 13, 3, 10, 17, 794, DateTimeKind.Local).AddTicks(8158));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 13, 3, 10, 17, 794, DateTimeKind.Local).AddTicks(8162));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Reservations");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 3, 15, 31, 7, 771, DateTimeKind.Local).AddTicks(4682));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 3, 15, 31, 7, 771, DateTimeKind.Local).AddTicks(4790));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 3, 15, 31, 7, 771, DateTimeKind.Local).AddTicks(4794));

            migrationBuilder.UpdateData(
                table: "Facilities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 3, 15, 31, 7, 771, DateTimeKind.Local).AddTicks(4799));
        }
    }
}
